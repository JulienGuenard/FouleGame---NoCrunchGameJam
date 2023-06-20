using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Attack : FlockAgent
{
                        public GameObject attack;
                        public float attackPreparation;
                        public float attackCooldown;
                        public int damage;

    [HideInInspector]   public FlockAgent targetAttacked;
    [HideInInspector]   public bool hasAttacked;
    [HideInInspector]   public bool isAttacking;

    private void Start()
    {
        hasAttacked = false;
        isAttacking = false;
    }

    private void Update()
    {
        if (agentSelection.isDragged)   AttackEnd();
        if (targetAttacked == null)     AttackEnd();
        if (this == null)               StopAllCoroutines();
        if (targetAttacked != null)
        {
            if (targetAttacked.agentSelection.isDragged) AttackEnd();
        }
    }

    public void AttackStart(FlockAgent target) 
    {
        if (targetAttacked == null) agentAnimation.AttackPrepStart();
        isAttacking = true;
        targetAttacked = target;

        if (!hasAttacked)           StartCoroutine(AttackPreparation()); 
    }

    public void AttackEnd()
    {
        if (!isAttacking && targetAttacked == null) return;

        isAttacking = false;
        targetAttacked = null;
        agentAnimation.AttackPrepEnd();
        StopCoroutine(AttackPreparation());
        agentAnimation.AttackEnd();
    }

    IEnumerator AttackPreparation()
    {
        hasAttacked = true;
        agentAnimation.AttackStart(1 / attackPreparation);
        yield return new WaitForSeconds(attackPreparation);
        agentAnimation.AttackEnd();
        AttackSpawn();
        StartCoroutine(AttackCooldown());
    }

    void AttackSpawn()
    {
        if (targetAttacked == null) return;

        AttackManager.instance.SpawnAttackAtTarget(targetAttacked, damage);
    }

    IEnumerator AttackCooldown()
    {
        agentAnimation.AttackPrepStart();
        yield return new WaitForSeconds(attackCooldown);
        agentAnimation.AttackPrepEnd();
        hasAttacked = false;
    }
}
