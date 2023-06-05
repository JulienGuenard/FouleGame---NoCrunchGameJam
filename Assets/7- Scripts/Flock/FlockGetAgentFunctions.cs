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
            if (c.CompareTag("Cursor"))     continue;
            if (c.transform.parent == null) continue;

            GetNeutralAgents(c);

            context.Add(c.transform);

        }

        return context;
    }

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
    }

    public bool GetAgents(FlockAgent agent, out FlockAgent target, AgentType agentType)
    {
        List<Transform> ennemis = new List<Transform>();
        Collider2D[] ennemisCollider = Physics2D.OverlapCircleAll(agent.transform.position, FCharge.radius);

        foreach (Collider2D i in ennemisCollider)
        {
            FlockAgent iFlockAgent = i.transform.gameObject.GetComponent<FlockAgent>();

            if (iFlockAgent == null)                                                                    continue;
            if (i.transform.parent == null)                                                             continue;
            if (agent.agentOwnership.parentflock.FOwnership.isPlayer == iFlockAgent.agentOwnership.parentflock.FOwnership.isPlayer)   continue;
            if (FBehaviour.agents.Contains(iFlockAgent))                                                continue;
            if (agentType != AgentType.Agressif && FAggro.pourcentAggro > 70)                           continue;

            ennemis.Add(i.transform);
            target = iFlockAgent;
            return true;
        }
        target = null;
        return false;
    }
}
