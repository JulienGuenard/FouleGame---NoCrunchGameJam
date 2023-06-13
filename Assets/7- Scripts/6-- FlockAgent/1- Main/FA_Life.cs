using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Life : FlockAgent
{
    public int Health;
    public float takeDamageCooldown;

    public void CheckHP()
    {
        if (Health <= 0) agentOwnership.parentflock.FDeath.Death(this);
    }

    public void TakeDamage(AttackTarget atk)
    {
        if (atk.damage < 1) return;

        StartCoroutine(TakeDamageCooldown());
        this.Health -= atk.damage;
        CheckHP();
    }

    IEnumerator TakeDamageCooldown()
    {
        yield return new WaitForSeconds(takeDamageCooldown);
        triggerDamage.isDamaged = false;
    }

    public IEnumerator LifeTimer(int LifeTime)
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
}
