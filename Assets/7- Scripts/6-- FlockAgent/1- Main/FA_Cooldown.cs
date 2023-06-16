using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Cooldown : FlockAgent
{
    public bool canCheckEnemies = false;
    public bool canCalculateMove = false;

    public override void Awake()
    {
        base.Awake();
        canCheckEnemies = false;
        canCalculateMove = false;
    }

    public void UnableCalculateMove()
    {
        if (!canCalculateMove) return;

        canCalculateMove = false;
        StartCoroutine(CalculateMoveDelay());
    }

    public void UnableCheckEnemies()
    {
        if (!canCheckEnemies) return;

        canCheckEnemies = false;
        StartCoroutine(CheckEnemiesDelay());
    }

    IEnumerator CalculateMoveDelay()
    {
        yield return new WaitForSeconds(0.1f);
        canCalculateMove = true;
    }

    IEnumerator CheckEnemiesDelay()
    {
        yield return new WaitForSeconds(.1f);
        canCheckEnemies = true;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        UnableCheckEnemies();
    }
}
