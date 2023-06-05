using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockMovement : Flock
{
    [Header("Agent Movement")]
    [Range(0.05f, 100f)]    public float driveFactor = 10f;
    [Range(0f, 100f)]       public float maxSpeed = 5f;
    [Range(0.05f, 10f)]     public float neighborRadius = 1.5f;
    [Range(0f, 20f)]        public float avoidanceRadiusMultiplier = 0.5f;
    [HideInInspector]       public float squareMaxSpeed;
                            float squareNeighborRadius, squareAvoidanceRadius;
                            public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private void Start()
    {
        SquareNumberSetup();
    }

    void SquareNumberSetup()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
    }

    private void Update()
    {
        MoveEachAgent();
    }

    public void MoveEachAgent()
    {
        foreach (FlockAgent agent in FBehaviour.agents.ToArray())
        {
            if (agent.mouseInteraction.isSelected && FOwnership.chef == null) continue;

            FlockAgent target;

            DetectEnemy(agent, out target);
            MovementBehaviour(agent, target);

            agent.Move(agent.move);
        }
    }

    void DetectEnemy(FlockAgent agent, out FlockAgent target)
    {
        target = null;

        if (agent.canCheckEnemies && (FAggro.Target == null || FAggro.Target.Health <= 0))
        {
            agent.UnableCheckEnemies();
            FCharge.ennemis = FGetAgentFunctions.GetAgents(agent, out target, FType.agentType);
        }

        if (target != null) FAggro.Target = target;
    }

    void MovementBehaviour(FlockAgent agent, FlockAgent target)
    {
        if (FCharge.ennemis == true && FAggro.Target != null)
        {
            if (FType.agentType == AgentType.Agressif)  Charge(agent);
            if (FType.agentType == AgentType.Passif)    Convert(agent, target);
        }
        else NextMove_NextPoint(agent);
    }

    void NextMove_NextPoint(FlockAgent agent)
    {
        float distance = Vector2.Distance(FOwnership.chef.transform.position, agent.transform.position);
        float speed = Mathf.Clamp(distance, 1, maxSpeed);

        if (!agent.canCalculateMove) return;

        agent.UnableCalculateMove();
        List<Transform> context = FGetAgentFunctions.GetNearbyObjects(agent);
        agent.move = FBehaviour.flockAgentBehaviour.CalculateMove(agent, context, this, FOwnership.chef.transform.position);
        agent.move *= driveFactor;

        if (agent.move.sqrMagnitude > squareMaxSpeed) agent.move = agent.move.normalized * speed;
    }

    void Charge(FlockAgent agent)
    {
        StartCoroutine(FCharge.ChargedAttack(agent));
    }

    void Convert(FlockAgent agent, FlockAgent target)
    {
        FConversion.HasConvert = false;
        FConversion.MaxConvert = 100;

        if (FAggro.Target.tag == "agressif") return;

        if (FConversion.HasConvert == false && FAggro.Target != null)   StartCoroutine(FConversion.ConvertOther(FAggro.Target.gameObject, agent));
        else                                                            agent.Move(-FFear.Fear(agent.transform.position - target.transform.position) * 2);
    }
}
