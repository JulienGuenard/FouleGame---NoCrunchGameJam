using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Ownership : FlockAgent
{
    public Flock parentflock;
    public bool isPlayer;

    public void Initialize(Flock flock)
    {
        parentflock = flock;
        isPlayer = parentflock.FOwnership.isPlayer;
    }
}
