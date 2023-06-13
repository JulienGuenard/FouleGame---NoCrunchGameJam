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

    public void MoveEachAgent() // Appel� par UpdateEvent (voir inspector)
    {
        if (FOwnership.chef == null) return;

        foreach (FlockAgent agent in FBehaviour.agents.ToArray())
        {
            if (agent == null)                              continue;
            if (agent.agentSelection.isDragged)             continue;
            if (agent.agentAggro.targetOnAggro != null)     continue;
            
            NextPoint(agent);
            agent.agentMovement.Move(agent.agentMovement.move);
        }
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
