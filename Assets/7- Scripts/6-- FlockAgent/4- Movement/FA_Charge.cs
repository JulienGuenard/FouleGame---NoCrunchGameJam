using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Charge : FlockAgent
{
    public float attackRange;
    public float chargeSpeed;

    public void Charge()
    {
        float targetPosX = agentAggro.targetOnAggro.transform.position.x;
        float targetPosY = agentAggro.targetOnAggro.transform.position.y;
        float posX = transform.position.x;
        float posY = transform.position.y;

        Vector2 directionToTarget = new Vector2(targetPosX - posX, targetPosY - posY).normalized;
        directionToTarget *= chargeSpeed;

        float ennemiDistance = Vector2.Distance(agentAggro.targetOnAggro.transform.position, transform.position);

        if (ennemiDistance > attackRange) return;

       // agent.agentAggro.targetOnAggro.transform.GetComponent<FlockAgent>().agentLife.TakeDamage(damage);
        agentMovement.Move(directionToTarget);
    }
}
