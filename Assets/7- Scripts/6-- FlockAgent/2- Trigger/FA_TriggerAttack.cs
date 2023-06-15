using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_TriggerAttack : MonoBehaviour
{
    [HideInInspector]   public FlockAgent agentMain;

                        FlockAgent target;

    [HideInInspector] public CircleCollider2D circleCollider;

    private void Awake()
    {
        agentMain = GetComponentInParent<FlockAgent>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (agentMain.agentCooldown.canCheckEnemies)    circleCollider.enabled = true;
        else                                            circleCollider.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag != "agressif" && col.tag != "passif")                           return;

        target = col.GetComponent<FlockAgent>();

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
