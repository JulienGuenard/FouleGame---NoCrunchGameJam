using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Trigger : MonoBehaviour
{
    [HideInInspector] public FlockAgent agentMain;

    private void Awake()
    {
        agentMain = GetComponentInParent<FlockAgent>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag != "agressif" && col.tag != "passif")   return;
        if (agentMain.agentAggro.targetOnAggro != null)     return;

        agentMain.agentAggro.DetectEnemy(col.GetComponent<FlockAgent>());
    }
}
