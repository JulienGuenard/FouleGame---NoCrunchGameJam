using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockGetAgentFunctions : Flock
{
    public List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, FMovement.neighborRadius);

        foreach (Collider2D c in contextColliders)
        {
            if (c == agent.AgentCollider && c.CompareTag("Cursor") && !c.transform.parent != null) continue;

            if (c.transform.parent.CompareTag("Neutre") && FLifetime.addnew && FOwnership.isPlayer)
            {
                Flock flockpassif = PlayerManager.instance.flockPaco;
                Flock flockaggressif = PlayerManager.instance.flockAggro;
                FlockAgent cflockAgent = c.GetComponent<FlockAgent>();
                Flock cflockParent = cflockAgent.parentflock;

                cflockParent.FBehaviour.agents.Remove(cflockAgent);

                if (c.transform.tag == "passif")
                {
                    c.transform.SetParent(flockpassif.transform, true);
                    flockpassif.FBehaviour.agents.Add(cflockAgent);
                    FLifetime.addnew = false;
                }

                if (c.transform.tag == "agressif")
                {
                    c.transform.SetParent(flockaggressif.transform, true);
                    flockaggressif.FBehaviour.agents.Add(cflockAgent);
                    FLifetime.addnew = false;
                }
            }

            context.Add(c.transform);

        }

        return context;
    }

    public bool GetAgents(FlockAgent agent, out FlockAgent target, bool getPassif, bool getEnemies)
    {
        List<Transform> ennemis = new List<Transform>();
        Collider2D[] ennemisCollider = Physics2D.OverlapCircleAll(agent.transform.position, FCharge.radius);

        foreach (Collider2D i in ennemisCollider)
        {
            FlockAgent iFlockAgent = i.transform.gameObject.GetComponent<FlockAgent>();
            if ((iFlockAgent != null && i.transform.parent != null && FOwnership.isPlayer)
                && (getEnemies || (getPassif && i.transform.tag == "passif")))
            {
                    if (FAggro.pourcentAgro <= 70 || getPassif)
                    {
                        if (!FBehaviour.agents.Contains(iFlockAgent) && i.transform.parent.tag != "Neutre"
                         && i.transform.parent.tag != "Untagged" && i.transform.parent.tag != "Cursor")
                        {
                            ennemis.Add(i.transform);
                            target = iFlockAgent;
                            return true;
                        }
                    }
                    else
                    {
                        if (i.transform.parent.tag != "Neutre" && i.transform.parent.tag != "Untagged" && i.transform.parent.tag != "Cursor")
                        {
                            if (getEnemies || (getPassif && !FBehaviour.agents.Contains(iFlockAgent) && i.transform.parent.tag != "Ennemi"))
                            {
                                ennemis.Add(i.transform);
                                target = iFlockAgent;
                                return true;
                            }
                        }
                    }
                }

            if (getEnemies || (getPassif && i.transform.tag == "agressif" && i.transform.parent.tag != "PlayerFlock"))
            {
                if (!FBehaviour.agents.Contains(iFlockAgent) && i.transform.parent.tag != "Neutre"
                    && i.transform.parent.tag != "Untagged" && i.transform.parent.tag != "Cursor"
                        && ((getEnemies && i.transform.parent.tag != "Ennemi")
                        || (getPassif && i.transform.parent.tag != "PlayerFlock")))
                {

                    ennemis.Add(i.transform);
                    target = iFlockAgent;

                    return true;
                }
            }
        }
        target = null;
        return false;
    }
}
