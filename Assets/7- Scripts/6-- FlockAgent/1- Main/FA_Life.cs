using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Life : FlockAgent
{
    public int Health = 100;

    public void CheckHP()
    {
        if (Health <= 0) agentOwnership.parentflock.FDeath.Death(this);
    }

    public void TakeDamage(int Dmg)
    {
        this.Health -= Dmg;
        CheckHP();
    }

    public IEnumerator LifeTimer(int LifeTime)
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
}
