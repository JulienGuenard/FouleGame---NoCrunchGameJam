using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlockLifetime : Flock
{
    public void IncrementTimedDead()
    {
        if (!addnew)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                addnew = true;
                timer = TIMER;
            }
        }
    }

    public void TimedDead()
    {
        compteur = agents.Count;
        if (isPlayer && agents.Count != 0)
        {
            timer2 -= Time.deltaTime;
            if (timer2 <= 0)
            {
                agents.First().flockAgentAnimation.DeadAnimation();

                Destroy(agents.First().gameObject);
                agents.Remove(agents.First());

                timer2 = timebtwDeath;
            }
        }
    }
}
