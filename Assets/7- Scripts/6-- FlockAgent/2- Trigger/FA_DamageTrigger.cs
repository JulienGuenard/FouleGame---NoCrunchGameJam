using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_DamageTrigger : MonoBehaviour
{
    [HideInInspector] public FlockAgent agentMain;

    private void Awake()
    {
        agentMain = GetComponentInParent<FlockAgent>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag != "attack") return;

        agentMain.agentLife.TakeDamage(1);
    }
}
