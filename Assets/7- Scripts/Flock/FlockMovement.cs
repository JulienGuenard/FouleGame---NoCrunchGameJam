using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockMovement : Flock
{
    [Header("Agent Movement")]
    [Range(0.05f, 100f)] public float driveFactor = 10f;
    [Range(0f, 100f)] public float maxSpeed = 5f;
    [Range(0.05f, 10f)] public float neighborRadius = 1.5f;
    [Range(0f, 20f)] public float avoidanceRadiusMultiplier = 0.5f;
    [HideInInspector] public float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private void Start()
    {
        MovementSetup();
    }

    private void Update()
    {
        GroupMovement();
    }

    void MovementSetup()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
    }

    public void GroupMovement()
    {
        foreach (FlockAgent agent in FBehaviour.agents.ToArray())
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
            if (agent != null && FOwnership.chef != null)
            {
                if (!agent.mouseInteraction.isSelected)
                {

                    agent.CheckHP();
                    

                    if (!FType.isPacifique && !FType.isAgressif)
                    {
                        NextMove(agent);
                    }else
                    {
                        NextMovePassif(agent);
                    }
                    

                    /*
                    if (FType.isAgressif)
                    {
                        Agressif(agent);

                    }
                    else if (FType.isPacifique)
                    {
                        Pacifique(agent);
                    }
                    else if (!FType.isPacifique && !FType.isAgressif)
                    {
                        Passif(agent);
                    }
                    */
                }
            }

            // Conversion
            if ((FOwnership.chef == null && agent != null) || (agent.ConvertPercent == 100 && agent != null))
            {
                if (agent.ConvertPercent > 0)
                {
                    agent.ConvertPercent = 0;
                }
                FBehaviour.agents.Remove(agent);
                agent.transform.SetParent(GameManager.neutre.transform, true);
                GameManager.neutreFlock.FBehaviour.agents.Add(agent);
                if (FBehaviour.agents.Count == 0)
                {
                    Destroy(gameObject);
                }
            }

            // Remove null agent
            if (agent == null)
            {
                FBehaviour.agents.Remove(agent);
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

        }
        return context;
    }

    bool GetEnnemis(FlockAgent agent, out FlockAgent target)
    {
        List<Transform> ennemis = new List<Transform>();
        Collider2D[] ennemisCollider = Physics2D.OverlapCircleAll(agent.transform.position, FCharge.radius);
        foreach (Collider2D i in ennemisCollider)
        {
            FlockAgent iFlockAgent = i.transform.gameObject.GetComponent<FlockAgent>();
            if (iFlockAgent != null && i.transform.parent != null)
            {
                if (FOwnership.isPlayer)
                {
                    if (FAggro.pourcentAgro <= 70)
                    {
                        if (!FBehaviour.agents.Contains(iFlockAgent) && i.transform.parent.tag != "Neutre" && i.transform.parent.tag != "PlayerFlock"
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

                    if (!FBehaviour.agents.Contains(iFlockAgent) && i.transform.parent.tag != "Neutre" && i.transform.parent.tag != "Ennemi"
                        && i.transform.parent.tag != "Untagged" && i.transform.parent.tag != "Cursor")
                    {

                        ennemis.Add(i.transform);
                        target = iFlockAgent;

                        return true;
                    }
                }




            }
            if (i.transform.tag == "Chefs" && FOwnership.isPlayer)
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
        Collider2D[] ennemisCollider = Physics2D.OverlapCircleAll(agent.transform.position, FCharge.radius);
        foreach (Collider2D i in ennemisCollider)
        {
            FlockAgent iFlockAgent = i.transform.gameObject.GetComponent<FlockAgent>();
            if (iFlockAgent != null && i.transform.parent != null && i.transform.tag == "passif")
            {
                if (FOwnership.isPlayer)
                {


                    if (!FBehaviour.agents.Contains(iFlockAgent) && i.transform.parent.tag != "Neutre" && i.transform.parent.tag != "PlayerFlock"
                     && i.transform.parent.tag != "Untagged" && i.transform.parent.tag != "Cursor")
                    {
                        ennemis.Add(i.transform);
                        target = iFlockAgent;
                        return true;
                    }
                }
                else
                {

                    if (!FBehaviour.agents.Contains(iFlockAgent) && i.transform.parent.tag != "Neutre" && i.transform.parent.tag != "Ennemi"
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
                if (!FBehaviour.agents.Contains(iFlockAgent) && i.transform.parent.tag != "Neutre" && i.transform.parent.tag != "PlayerFlock"
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

    void NextMove(FlockAgent agent)
    {
        float distance = Vector2.Distance(FOwnership.chef.transform.position, agent.transform.position);
        float Speed = Mathf.Clamp(distance, 1, maxSpeed);
        FlockAgent target = null;

        if (agent.canCheckEnemies)
        {
            agent.UnableCheckEnemies();
            if (FType.isAgressif) FCharge.ennemis = GetEnnemis(agent, out target);
            if (FType.isPacifique) FCharge.ennemis = GetPassif(agent, out target);
        }

        if (target != null)
        {
            FAggro.Target = target;
        }

        if (FCharge.ennemis == true && FAggro.Target != null)
        {
            if (FType.isAgressif) NextMoveAgressif(agent);
            if (FType.isPacifique) NextMovePacifique(agent, target);
        }
        else
        {
            if (agent.canCalculateMove)
            {
                agent.UnableCalculateMove();
                List<Transform> context = GetNearbyObjects(agent);
                agent.move = FBehaviour.flockAgentBehaviour.CalculateMove(agent, context, this, FOwnership.chef.transform.position);
                agent.move *= driveFactor;
                if (agent.move.sqrMagnitude > squareMaxSpeed)
                {
                    agent.move = agent.move.normalized * Speed;
                }
            }
            agent.Move(agent.move);
        }
    }

    void NextMoveAgressif(FlockAgent agent)
    {
        StartCoroutine(FCharge.ChargedAttack(agent));
    }

    void NextMovePacifique(FlockAgent agent, FlockAgent target)
    {
        FConversion.HasConvert = false;
        FConversion.MaxConvert = 100;
        if (FAggro.Target.tag != "agressif")
        {
            if (FConversion.HasConvert == false && FAggro.Target != null)
            {
                StartCoroutine(FConversion.ConvertOther(FAggro.Target.gameObject, agent));
            }
        }
        else
        {
            agent.Move(-FFear.Fear(agent.transform.position - target.transform.position) * 2);
        }
    }

    void NextMovePassif(FlockAgent agent)
    {
        float distance = Vector2.Distance(FOwnership.chef.transform.position, agent.transform.position);
        float Speed = Mathf.Clamp(distance, 1, maxSpeed);
        List<Transform> context = GetNearbyObjects(agent);
        Vector2 move = FBehaviour.flockAgentBehaviour.CalculateMove(agent, context, this, FOwnership.chef.transform.position);
        move *= driveFactor;
        if (move.sqrMagnitude > squareMaxSpeed)
        {
            move = move.normalized * Speed;
        }
        agent.Move(move);
    }
}
