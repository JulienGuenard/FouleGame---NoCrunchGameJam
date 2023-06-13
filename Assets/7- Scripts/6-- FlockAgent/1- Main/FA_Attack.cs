using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Attack : FlockAgent
{
    public GameObject attack;
    public float attackCooldown;
    public int damage;

    public void Attack(FlockAgent target)
    {
        if (target.agentOwnership.isPlayer == agentOwnership.isPlayer) return;

        StartCoroutine(AttackCooldown());
        AttackSpawn(target);
    }

    void AttackSpawn(FlockAgent target)
    {
        GameObject  atk = Instantiate(attack, target.transform.position, Quaternion.identity);
                    atk.GetComponent<DestroyAfterTime>().delay = 1f;

        AttackTarget    atkTarget = atk.GetComponent<AttackTarget>(); 
                        atkTarget.target = target;
                        atkTarget.damage = damage;
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        triggerAttack.hasAttacked = false;
    }
}
