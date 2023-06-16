using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockGetAgentFunctions : Flock
{
    public List<Transform> GetNNearbyAgents(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, FMovement.neighborRadius);

        foreach (Collider2D c in contextColliders)
        {
            if (c == agent.AgentCollider)   continue;
            if (c.gameObject.layer != 3)    continue;

        //    GetNeutralAgents(c);

            context.Add(c.transform);

        }

        return context;
    }
    /*
    void GetNeutralAgents(Collider2D c)
    {
        if (c.transform.parent.CompareTag("Neutre") && FOwnership.isPlayer)
        {
            Flock flockpassif = PlayerManager.instance.flockPaco;
            Flock flockaggressif = PlayerManager.instance.flockAggro;
            FlockAgent cflockAgent = c.GetComponent<FlockAgent>();
            Flock cflockParent = cflockAgent.agentOwnership.parentflock;

            cflockParent.FBehaviour.agents.Remove(cflockAgent);

            if (c.transform.tag == "passif")
            {
                c.transform.SetParent(flockpassif.transform, true);
                flockpassif.FBehaviour.agents.Add(cflockAgent);
            }

            if (c.transform.tag == "agressif")
            {
                c.transform.SetParent(flockaggressif.transform, true);
                flockaggressif.FBehaviour.agents.Add(cflockAgent);
            }
        }
    }*/
}
