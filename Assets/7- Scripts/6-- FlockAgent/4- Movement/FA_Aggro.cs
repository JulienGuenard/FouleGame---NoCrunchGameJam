using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Aggro : FlockAgent
{
    public FlockAgent targetOnAggro;

    private void Update()
    {
        if (targetOnAggro != null) agentCharge.Charge();
    }

    public void DetectEnemy(FlockAgent target)
    {
        targetOnAggro = target;
        
    }

    public void ChaseAnotherEntity(FlockAgent other, FlockAgent agent)
    {
        float Distance = Vector2.Distance(agent.transform.position, other.transform.position);
        Vector2 direction = other.transform.position - agent.transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        agent.transform.position = Vector2.MoveTowards(agent.transform.position, other.transform.position, 1 * Time.deltaTime);
        agent.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
