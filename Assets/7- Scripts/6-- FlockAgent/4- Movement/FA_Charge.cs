using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Charge : FlockAgent
{
    public float chargePreparation;
    public float attackRange;
    public float chargeSpeed;

    bool isPrepCharging = false;
    bool isCharging     = false;

    public void ChargeStart()
    {
        if (isCharging) {               Charge(); return; }
        if (!isPrepCharging)            StartCoroutine(ChargePreparation());
        if (agentAttack.isAttacking)    ChargeEnd();
    }

    public void ChargeEnd()
    {
        isPrepCharging = false;
        isCharging = false;
        agentAnimation.ChargeEnd();
    }

    void Charge()
    {
        agentAnimation.ChargeStart();

        float targetPosX = agentAggro.targetOnAggro.transform.position.x;
        float targetPosY = agentAggro.targetOnAggro.transform.position.y;
        float posX = transform.position.x;
        float posY = transform.position.y;

        Vector2 directionToTarget = new Vector2(targetPosX - posX, targetPosY - posY).normalized;
        directionToTarget *= chargeSpeed;

        float ennemiDistance = Vector2.Distance(agentAggro.targetOnAggro.transform.position, transform.position);

        if (ennemiDistance > attackRange) return;

        agentMovement.Move(directionToTarget);
    }

    IEnumerator ChargePreparation()
    {
        isPrepCharging = true;
        agentAnimation.ChargePrepStart();
        yield return new WaitForSeconds(chargePreparation);
        isCharging = true;
        isPrepCharging = false;
        agentAnimation.ChargePrepEnd();
        
    }
}
