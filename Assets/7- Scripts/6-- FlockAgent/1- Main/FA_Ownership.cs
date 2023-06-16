using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Ownership : FlockAgent
{
    public Flock parentflock;
    public bool isPlayer;

    public Material[] matColorSwap;
    public Material[] matPlayer;

    public void Initialize(Flock flock)
    {
        parentflock = flock;
        isPlayer = parentflock.FOwnership.isPlayer;
        SwapColor();
        ChangeLayer();
    }

    public void ChangeLayer()
    {

        if (isPlayer)
        {
          //  gameObject.layer = 10;
            if (triggerAggro != null) triggerAggro.gameObject.layer = 10;
            if (triggerAttack != null) triggerAttack.gameObject.layer = 10;
            if (triggerDamage != null) triggerDamage.gameObject.layer = 10;
        }
        else
        {
        //    gameObject.layer = 11;
            if (triggerAggro != null) triggerAggro.gameObject.layer = 11;
            if (triggerAttack != null) triggerAttack.gameObject.layer = 11;
            if (triggerDamage != null) triggerDamage.gameObject.layer = 11;
        }
    }

    public void ChangeLayerAlone(GameObject obj)
    {
        if (isPlayer)
        {
            if (obj != null) obj.layer = 10;
        }
        else
        {
            if (obj != null) obj.layer = 11;
        }
    }

    public void SwapColor()
    {
        if (!agentOwnership.isPlayer) spriteR.materials = matColorSwap;
        if (agentOwnership.isPlayer) spriteR.materials = matPlayer;
    }
}
