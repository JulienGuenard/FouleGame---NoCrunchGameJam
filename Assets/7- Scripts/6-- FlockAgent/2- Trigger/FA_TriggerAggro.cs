using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_TriggerAggro : MonoBehaviour
{
    [HideInInspector]   public FlockAgent agentMain;
    [HideInInspector]   public CircleCollider2D circleCollider;

                        bool canAggro = true;

    private void Awake()
    {
        agentMain       = GetComponentInParent<FlockAgent>();
        circleCollider  = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (agentMain.agentAggro.targetOnAggro == null) canAggro = true;
        else                                            canAggro = false;

        if (agentMain.agentCooldown.canCheckEnemies && canAggro)    circleCollider.enabled = true;
        else                                                        circleCollider.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (agentMain.agentAggro.targetOnAggro != null) {                                   canAggro = false; circleCollider.enabled = false; }
        if (col.tag != "agressif" && col.tag != "passif")                                   return;
        if (agentMain.agentOwnership.isPlayer == col.GetComponent<FA_Ownership>().isPlayer) return;

        agentMain.agentAggro.DetectEnemy(col.GetComponent<FlockAgent>());
    }
}
