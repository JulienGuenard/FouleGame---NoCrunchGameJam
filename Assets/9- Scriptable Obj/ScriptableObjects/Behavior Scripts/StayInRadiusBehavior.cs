using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay in Radius")]
public class StayInRadiusBehavior : FA_Behaviour
{
    public float radius = 15f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock,Vector2 chefpos)
    {
        Vector2 centerOffset = chefpos - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;
        if(t < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset * t * t;
    }
}
