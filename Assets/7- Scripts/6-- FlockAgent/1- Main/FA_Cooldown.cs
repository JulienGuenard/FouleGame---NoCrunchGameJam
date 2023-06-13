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
        yield return new WaitForSeconds(8f * Time.deltaTime);
        canCalculateMove = true;
    }

    IEnumerator CheckEnemiesDelay()
    {
        yield return new WaitForSeconds(40f * Time.deltaTime);
        canCheckEnemies = true;
    }
}
