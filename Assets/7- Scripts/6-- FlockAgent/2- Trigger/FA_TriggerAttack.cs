using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_TriggerAttack : MonoBehaviour
{
    [HideInInspector]   public bool hasAttacked = false;

    [HideInInspector]   public FlockAgent agentMain;

    private void Awake()
    {
        agentMain = GetComponentInParent<FlockAgent>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (hasAttacked)                                                            return;
        if (col.tag != "agressif" && col.tag != "passif")                           return;

        FlockAgent target = col.GetComponent<FlockAgent>();

        if (target.agentOwnership.isPlayer == agentMain.agentOwnership.isPlayer)    return;

        hasAttacked = true;
        agentMain.agentAttack.Attack(target);
    }
}
