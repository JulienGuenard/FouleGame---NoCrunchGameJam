using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_TriggerDamage : MonoBehaviour
{
    [HideInInspector]   public bool isDamaged = false;

    [HideInInspector]   public FlockAgent agentMain;
    [HideInInspector]   public CircleCollider2D circleCollider;

    bool canTrigger = true;

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
        if (!canTrigger)            return;
        if (isDamaged)              return;
        if (col.tag != "Attack")    return;
        AttackTarget attackTarget = col.GetComponent<AttackTarget>();

        if (attackTarget.damage == 0)           return;
        if (attackTarget.target != agentMain)   return;
        isDamaged = true;
        canTrigger = false;
        agentMain.agentLife.TakeDamage(attackTarget);
    }
}
