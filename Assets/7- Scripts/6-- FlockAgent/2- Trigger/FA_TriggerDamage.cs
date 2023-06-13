using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_TriggerDamage : MonoBehaviour
{
    [HideInInspector]   public bool isDamaged = false;

    [HideInInspector]   public FlockAgent agentMain;

    private void Awake()
    {
        agentMain = GetComponentInParent<FlockAgent>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (isDamaged)                                              return;
        if (col.tag != "Attack")                                    return;

        AttackTarget attackTarget = col.GetComponent<AttackTarget>();

        if (attackTarget.target != agentMain)   return;

        isDamaged = true;
        agentMain.agentLife.TakeDamage(attackTarget);
    }
}
