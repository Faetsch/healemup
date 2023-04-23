using System.Collections;
using UnityEngine;

/*
 * Bossskript für Demo Boss
 * TODO - work in progress - bisher nur einfaches Zuschlagen
 */
public class LarryTheSkellyBehaviour : BossAttack
{
    [SerializeField] float hitDamage = 15f;
    Animator animator;

    public void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public override void DefaultAttack()
    {
        TwoHandedAttackAnimation();
    }

    //Wird in keyframe der Animation aufgerufen
    public void TwoHandedAttackDamage()
    {
        target?.GetComponent<UnitHealth>().TakeDamage(hitDamage);
    }

    //TODO in event und eigenen Handler auslagern
    private void TwoHandedAttackAnimation()
    {
        animator.SetInteger("WeaponType_int", 12);
        animator.SetInteger("MeleeType_int", 2);
    }
}