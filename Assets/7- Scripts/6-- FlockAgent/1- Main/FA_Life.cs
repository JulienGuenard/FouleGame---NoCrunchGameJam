using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Life : FlockAgent
{
    public int Health;
    public float takeDamageCooldown;
    public float takeDamageDelay;

    public void TakeDamage(AttackTarget atk) { StartCoroutine(DamageAfterDelay(atk)); }

    IEnumerator DamageAfterDelay(AttackTarget atk)
    {
        agentAnimation.DamagedStart();
        yield return new WaitForSeconds(takeDamageDelay);
        agentAnimation.DamagedEnd();
        this.Health -= atk.damage;
        CheckHP();
        StartCoroutine(TakeDamageCooldown());
    }

    public void CheckHP() { if (Health <= 0) agentOwnership.parentflock.FDeath.Death(this); }

    IEnumerator TakeDamageCooldown()
    {
        yield return new WaitForSeconds(takeDamageCooldown);
        triggerDamage.isDamaged = false;
    }
}