using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steere Cohesion")]
public class SteereCohesionBehavior : FilteredFlockBehaviour
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, Vector2 Chefpos)
    {
        //if no neighbors, return no adjustment 
        if (context.Count == 0)
            return Vector2.zero;

        //add all points together and average
        Vector2 cohesionMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= filteredContext.Count;

        //create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;
        currentVelocity = agent.agentMovement.velocity;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }

}

