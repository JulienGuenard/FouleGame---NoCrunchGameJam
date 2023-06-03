using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockMovement : Flock
{
    public IEnumerator ChargedAttack(FlockAgent agent)
    {
        yield return new WaitForSeconds(ChargedTime);

        if (agent != null)
        {
            StartCoroutine(Charge(agent));
        }
    }

    public IEnumerator Charge(FlockAgent agent)
    {
        if (Target != null)
        {
            float LaucnhDirectionX = Target.transform.position.x;
            float LaucnhDIrectionY = Target.transform.position.y;
            float posX = agent.transform.position.x;
            float posY = agent.transform.position.y;

            Vector2 lauchDir = new Vector2(LaucnhDirectionX - posX, LaucnhDIrectionY - posY).normalized;
            DistancePos = lauchDir;
            agent.Move(lauchDir * LauchForce);
            IsLaunch = true;

            agent.flockAgentAnimation.ChargeAnimation();
        }
        else
        {
            agent.flockAgentAnimation.EndChargeAnimation();
        }


        yield return new WaitForSeconds(ChargedTime);
        if (agent != null && Target != null)
        {
            Ennemidistance = Vector2.Distance(Target.transform.position, agent.transform.position);
        }


        if (Ennemidistance <= 0.2 && Target != null && agent != null)
        {
            CheckAttack(agent);
        }
        IsLaunch = false;


        ennemis = false;
    }

    public void GroupMovement()
    {
        foreach (FlockAgent agent in agents.ToArray())
        {
            // Passif Conversion
            if (this.tag == "Neutre")
            {
                if (agent.ConvertPercent > 0)
                {
                    agent.ConvertPercent = 0;
                }
            }

            // Movement Behaviour
            if (agent != null && chef != null)
            {
                if (!agent.mouseInteraction.isSelected)
                {

                    agent.CheckHP();
                    if (isAgressif)
                    {
                        Agressif(agent);

                    }
                    else if (isPacifique)
                    {
                        Pacifique(agent);
                    }
                    else if (!isPacifique && !isAgressif)
                    {
                        Passif(agent);
                    }

                }
            }

            // Conversion
            if ((chef == null && agent != null) || (agent.ConvertPercent == 100 && agent != null))
            {
                if (agent.ConvertPercent > 0)
                {
                    agent.ConvertPercent = 0;
                }
                agents.Remove(agent);
                agent.transform.SetParent(GameManager.neutre.transform, true);
                GameManager.neutreFlock.agents.Add(agent);
                if (agents.Count == 0)
                {
                    Destroy(gameObject);
                }
            }

            // Remove null agent
            if (agent == null)
            {
                agents.Remove(agent);
            }
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider && !c.CompareTag("Cursor") && c.transform.parent != null)
            {

                if (c.transform.parent.CompareTag("Neutre") && addnew && isPlayer)
                {
                    Flock flockpassif = PlayerManager.instance.flockPaco;
                    Flock flockaggressif = PlayerManager.instance.flockAggro;
                    FlockAgent cflockAgent = c.GetComponent<FlockAgent>();
                    Flock cflockParent = cflockAgent.parentflock;

                    cflockParent.agents.Remove(cflockAgent);
                    if (c.transform.tag == "passif")
                    {
                        c.transform.SetParent(flockpassif.transform, true);
                        flockpassif.agents.Add(cflockAgent);
                        addnew = false;
                    }
                    if (c.transform.tag == "agressif")
                    {
                        c.transform.SetParent(flockaggressif.transform, true);
                        flockaggressif.agents.Add(cflockAgent);
                        addnew = false;
                    }

                }
                context.Add(c.transform);
            }

        }
        return context;
    }

    bool GetEnnemis(FlockAgent agent, out FlockAgent target)
    {
        List<Transform> ennemis = new List<Transform>();
        Collider2D[] ennemisCollider = Physics2D.OverlapCircleAll(agent.transform.position, radius);
        foreach (Collider2D i in ennemisCollider)
        {
            FlockAgent iFlockAgent = i.transform.gameObject.GetComponent<FlockAgent>();
            if (iFlockAgent != null && i.transform.parent != null)
            {
                if (isPlayer)
                {
                    if (pourcentAgro <= 70)
                    {
                        if (!agents.Contains(iFlockAgent) && i.transform.parent.tag != "Neutre" && i.transform.parent.tag != "PlayerFlock"
                         && i.transform.parent.tag != "Untagged" && i.transform.parent.tag != "Cursor")
                        {
                            ennemis.Add(i.transform);
                            target = iFlockAgent;
                            return true;
                        }
                    }
                    else
                    {
                        if (i.transform.parent.tag != "Neutre"
                        && i.transform.parent.tag != "Untagged" && i.transform.parent.tag != "Cursor")
                        {
                            ennemis.Add(i.transform);
                            target = iFlockAgent;
                            return true;
                        }
                    }
                }
                else
                {

                    if (!agents.Contains(iFlockAgent) && i.transform.parent.tag != "Neutre" && i.transform.parent.tag != "Ennemi"
                        && i.transform.parent.tag != "Untagged" && i.transform.parent.tag != "Cursor")
                    {

                        ennemis.Add(i.transform);
                        target = iFlockAgent;

                        return true;
                    }
                }




            }
            if (i.transform.tag == "Chefs" && isPlayer)
            {
                ennemis.Add(i.transform);
                target = iFlockAgent;

                return true;
            }

        }
        target = null;
        return false;

    }

    bool GetPassif(FlockAgent agent, out FlockAgent target)
    {
        List<Transform> ennemis = new List<Transform>();
        Collider2D[] ennemisCollider = Physics2D.OverlapCircleAll(agent.transform.position, radius);
        foreach (Collider2D i in ennemisCollider)
        {
            FlockAgent iFlockAgent = i.transform.gameObject.GetComponent<FlockAgent>();
            if (iFlockAgent != null && i.transform.parent != null && i.transform.tag == "passif")
            {
                if (isPlayer)
                {


                    if (!agents.Contains(iFlockAgent) && i.transform.parent.tag != "Neutre" && i.transform.parent.tag != "PlayerFlock"
                     && i.transform.parent.tag != "Untagged" && i.transform.parent.tag != "Cursor")
                    {
                        ennemis.Add(i.transform);
                        target = iFlockAgent;
                        return true;
                    }
                }
                else
                {

                    if (!agents.Contains(iFlockAgent) && i.transform.parent.tag != "Neutre" && i.transform.parent.tag != "Ennemi"
                        && i.transform.parent.tag != "Untagged" && i.transform.parent.tag != "Cursor")
                    {

                        ennemis.Add(i.transform);
                        target = iFlockAgent;

                        return true;
                    }
                }

            }
            if (i.transform.tag == "agressif" && i.transform.parent.tag != "PlayerFlock")
            {
                if (!agents.Contains(iFlockAgent) && i.transform.parent.tag != "Neutre" && i.transform.parent.tag != "PlayerFlock"
                && i.transform.parent.tag != "Untagged" && i.transform.parent.tag != "Cursor")
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

    void Agressif(FlockAgent agent)
    {
        float distance = Vector2.Distance(chef.transform.position, agent.transform.position);
        float Speed = Mathf.Clamp(distance, 1, maxSpeed);
        FlockAgent target = null;

        if (agent.canCheckEnemies)
        {
            agent.UnableCheckEnemies();
            ennemis = GetEnnemis(agent, out target);
        }

        if (target != null)
        {
            Target = target;
        }

        if (ennemis == true && Target != null)
        {
            StartCoroutine(ChargedAttack(agent));
        }
        else
        {
            if (agent.canCalculateMove)
            {
                agent.UnableCalculateMove();
                List<Transform> context = GetNearbyObjects(agent);
                agent.move = flockAgentBehaviour.CalculateMove(agent, context, this, chef.transform.position);
                agent.move *= driveFactor;
                if (agent.move.sqrMagnitude > squareMaxSpeed)
                {
                    agent.move = agent.move.normalized * Speed;
                }
            }
            agent.Move(agent.move);
        }
    }

    void Pacifique(FlockAgent agent)
    {
        float distance = Vector2.Distance(chef.transform.position, agent.transform.position);
        float Speed = Mathf.Clamp(distance, 1, maxSpeed);
        FlockAgent target = null;

        if (agent.canCheckEnemies)
        {
            agent.UnableCheckEnemies();
            ennemis = GetPassif(agent, out target);
        }

        if (target != null)
        {
            if (target.transform.parent.tag == "PlayerFlock")
            {

            }
            Target = target;
        }

        if (ennemis == true && Target != null)
        {

            HasConvert = false;
            MaxConvert = 100;
            if (Target.tag != "agressif")
            {
                if (HasConvert == false && Target != null)
                {
                    StartCoroutine(ConvertOther(Target.gameObject, agent));
                }
            }
            else
            {
                agent.Move(-flockFear.Fear(agent.transform.position - target.transform.position) * 2);
            }
        }
        else
        {
            if (agent.canCalculateMove)
            {
                agent.UnableCalculateMove();
                List<Transform> context = GetNearbyObjects(agent);
                agent.move = flockAgentBehaviour.CalculateMove(agent, context, this, chef.transform.position);
                agent.move *= driveFactor;
                if (agent.move.sqrMagnitude > squareMaxSpeed)
                {
                    agent.move = agent.move.normalized * Speed;
                }
            }

            agent.Move(agent.move);
        }


    }
    void Passif(FlockAgent agent)
    {
        float distance = Vector2.Distance(chef.transform.position, agent.transform.position);
        float Speed = Mathf.Clamp(distance, 1, maxSpeed);
        List<Transform> context = GetNearbyObjects(agent);
        Vector2 move = flockAgentBehaviour.CalculateMove(agent, context, this, chef.transform.position);
        move *= driveFactor;
        if (move.sqrMagnitude > squareMaxSpeed)
        {
            move = move.normalized * Speed;
        }
        agent.Move(move);
    }

    public void CheckAttack(FlockAgent agent)
    {
        Target.transform.GetComponent<FlockAgent>().TakeDamage(DMG);
        agent.Move(-DistancePos * RepulseForce);
    }
}
