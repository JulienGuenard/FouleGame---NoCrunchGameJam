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
    }

    public void SwapColor()
    {
        if (!agentOwnership.isPlayer) spriteR.materials = matColorSwap;
        if (agentOwnership.isPlayer) spriteR.materials = matPlayer;
    }
}
