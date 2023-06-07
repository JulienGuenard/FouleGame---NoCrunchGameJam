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

    public void MoveEachAgent() // Appelé par FlockEvent (voir inspector)
    {
        if (FOwnership.chef == null) return;

        foreach (FlockAgent agent in FBehaviour.agents.ToArray())
        {
            if (agent.agentCursorInputs.isSelected) continue;

            FlockAgent target;
            DetectEnemy(agent, out target);
            MovementBehaviour(agent, target);

            if (agent == null) continue;

            agent.agentMovement.Move(agent.agentMovement.move);
        }
    }

    void DetectEnemy(FlockAgent agent, out FlockAgent target)
    {
        target = null;

        if (!agent.agentCooldown.canCheckEnemies) return;

        agent.agentCooldown.UnableCheckEnemies();
        FCharge.ennemis = FGetAgentFunctions.GetAgents(agent, out target, FType.agentType);
        FAggro.targetOnAggro = target;
    }

    void MovementBehaviour(FlockAgent agent, FlockAgent target)
    {
        if (FCharge.ennemis == true && FAggro.targetOnAggro != null)
        {
            if (FType.agentType == AgentType.Agressif)  FCharge.Charge(agent);
            if (FType.agentType == AgentType.Passif)    FConversion.Convert(agent, target);
        }
        else NextPoint(agent);
    }

    void NextPoint(FlockAgent agent)
    {
        if (agent == null) return;

        float distance = Vector2.Distance(FOwnership.chef.transform.position, agent.transform.position);
        float speed = Mathf.Clamp(distance, 1, maxSpeed);

        if (!agent.agentCooldown.canCalculateMove) return;

        agent.agentCooldown.UnableCalculateMove();
        List<Transform> context = FGetAgentFunctions.GetNNearbyAgents(agent);
        agent.agentMovement.move = FBehaviour.flockAgentBehaviour.CalculateMove(agent, context, this, FOwnership.chef.transform.position);
        agent.agentMovement.move *= driveFactor;

        if (agent.agentMovement.move.sqrMagnitude > squareMaxSpeed) agent.agentMovement.move = agent.agentMovement.move.normalized * speed;
    }
}
