using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_TriggerAttack : MonoBehaviour
{
    [HideInInspector]   public FlockAgent agentMain;

                        FlockAgent target;

    [HideInInspector] public CircleCollider2D circleCollider;

    bool isShaping = true;

    private void Awake()
    {
        agentMain = GetComponentInParent<FlockAgent>();
        circleCollider = GetComponent<CircleCollider2D>();

        StartCoroutine(GenerateShapes());
    }

    private void FixedUpdate()
    {
        if (isShaping) { circleCollider.enabled = true; return; }

        if (agentMain.agentCooldown.canCheckEnemies && agentMain.agentAggro.targetOnAggro != null)  circleCollider.enabled = true;
        else                                                                                        circleCollider.enabled = false;
    }

    IEnumerator GenerateShapes()
    {
        yield return new WaitForSeconds(1f);
        isShaping = false;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag != "agressif" && col.tag != "passif")                           return;

        target = col.GetComponentInParent<FlockAgent>();

        if (target.agentOwnership.isPlayer == agentMain.agentOwnership.isPlayer)    return;

        agentMain.agentAttack.AttackStart(target);
    }
/*
    private void OnTriggerExit2D(Collider2D col)
    {
       
        if (col.tag != "agressif" && col.tag != "passif")   return;
        if (col.GetComponent<FlockAgent>() != target)       return;

        target = null;
        agentMain.agentAttack.AttackEnd();
    }*/
}
