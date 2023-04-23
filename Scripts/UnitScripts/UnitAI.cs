using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform targetBoss;
    Vector3 target;
    float distanceToTarget = Mathf.Infinity;
    float chaseRange = 5f;
    float turnSpeed = 5f;
    Animator animator;
    UnitHealth unitHealth;
    AttackSlotManager targetSlotManager;
    int currentSlot = 0;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        targetBoss = GameObject.FindGameObjectWithTag("Boss").transform;
        animator = GetComponentInChildren<Animator>();
        targetSlotManager = targetBoss.GetComponent<AttackSlotManager>();
        unitHealth = GetComponent<UnitHealth>();
    }
    void Start()
    {
        currentSlot = targetSlotManager.Reserve(gameObject);
        target = targetSlotManager.GetSlotPosition(currentSlot);
    }

    void FixedUpdate()
    {
        if (!unitHealth.isDead())
        {
            distanceToTarget = Vector3.Distance(target, transform.position);
            if (distanceToTarget >= agent.stoppingDistance)
            {
                agent.SetDestination(target);
                animator.SetFloat("Speed_f", 10f);
            }
            if (distanceToTarget <= agent.stoppingDistance)
            {
                AttackTarget();
            }
        }
    }

    private void LateUpdate()
    {
        FaceTarget();
    }

    private void AttackTarget()
    {
        //TODO - Als event/listener auslagern, Animationen anpassen
        animator.SetFloat("Speed_f", 0f);
        animator.SetInteger("WeaponType_int", 12);
        animator.SetInteger("MeleeType_int", 2);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
