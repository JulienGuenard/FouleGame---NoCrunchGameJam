using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Ownership : FlockAgent
{
    public Flock parentflock;

    public void Initialize(Flock flock)
    {
        parentflock = flock;
    }
}
