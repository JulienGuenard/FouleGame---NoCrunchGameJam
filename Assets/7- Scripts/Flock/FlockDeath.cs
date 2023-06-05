using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockDeath : Flock
{
    public void Death(FlockAgent agent)
    {
        agent.agentAnimation.DeadAnimation();

            Destroy(agent.gameObject);
            FBehaviour.agents.Remove(agent);
    }
}
