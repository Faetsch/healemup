using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class BossAttack : MonoBehaviour
{
    public Transform target;
    float turnSpeed = 5f;
    float aggroRange = 30f;

    public void Start()
    {
        AcquireTarget();
    }

    private void AcquireTarget()
    {
        //Erst versuchen lebendigen Tank zu finden
        GameObject tank = GameObject.FindGameObjectWithTag("Tank");
        if(tank != null)
        {
            UnitHealth unitHealth = tank.GetComponent<UnitHealth>();
            if (!unitHealth.isDead())
            {
                target = tank.transform;
            }
        }

        else
        {
            //TODO - work in progress - vorerst irgendein lebendiges Target finden
            List<UnitHealth> candidates = (from u in new List<UnitHealth>(FindObjectsOfType<UnitHealth>())
                                           where !u.isDead()
                                           select u).ToList();

            if (candidates.Count > 0)
            {
                target = candidates.First().transform;
            } else
            {
                target = null;
            }
        }
    }


    public void FixedUpdate()
    {
        if (ShouldLookForNewTarget())
        {
            AcquireTarget();
        }
        else
        {
            AttackTarget();
        }
    }

    private bool ShouldLookForNewTarget()
    {
        if (target == null) { return true; }
        else
        {
            UnitHealth unitHealth = target.gameObject.GetComponent<UnitHealth>();
            return unitHealth.isDead();
        }
    }

    public void LateUpdate()
    {
        FaceTarget();
    }

    public void AttackTarget()
    {
        if (IsTargetInRange())
        {
            DefaultAttack();
        }
    }

    private bool IsTargetInRange()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        return distance < aggroRange;
    }

    public virtual void DefaultAttack()
    {
        //do nothing, wird in Unterklassen überschrieben
    }
    private void FaceTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
    }
}
