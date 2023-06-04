using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, Vector2 chefpos)
    {
        //if no neighbors, return no adjustment 
        if (context.Count == 0)
            return Vector2.zero;

        //add all points together and average
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.FMovement.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
            
        }
        if (nAvoid >0)
        {
            avoidanceMove /= nAvoid;
        }
        return avoidanceMove;
    }
}
