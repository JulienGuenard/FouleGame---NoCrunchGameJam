using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    [HideInInspector]   public FA_Animation         agentAnimation;
    [HideInInspector]   public FA_Life              agentLife;
    [HideInInspector]   public FA_Charge            agentCharge;
    [HideInInspector]   public FA_Conversion        agentConversion;
    [HideInInspector]   public FA_Ownership         agentOwnership;
    [HideInInspector]   public FA_Movement          agentMovement;
    [HideInInspector]   public FA_Cooldown          agentCooldown;
    [HideInInspector]   public FA_Physics           agentPhysics;
    [HideInInspector]   public FA_Selection         agentSelection;
    [HideInInspector]   public FA_Sprite            agentSprite;
    [HideInInspector]   public FA_Aggro             agentAggro;
    [HideInInspector]   public FA_Attack            agentAttack;

    [HideInInspector]   public FA_TriggerAggro      triggerAggro;
    [HideInInspector]   public FA_TriggerDamage     triggerDamage;
    [HideInInspector]   public FA_TriggerAttack     triggerAttack;

    [HideInInspector]   public FlockAgent           agentMain;
    [HideInInspector]   public Rigidbody2D          rb;
    [HideInInspector]   new public Collider2D       collider;
                        public Collider2D           AgentCollider { get { return collider; } }
                        Flock                       agentFlock;
                        public Flock                AgentFlock { get { return agentFlock; } }
    [HideInInspector]   public SpriteRenderer       spriteR;

    public virtual void Awake()
    {
        agentAnimation      = GetComponent<FA_Animation>();
        agentLife           = GetComponent<FA_Life>();
        agentCharge         = GetComponent<FA_Charge>();
        agentConversion     = GetComponent<FA_Conversion>();
        agentOwnership      = GetComponent<FA_Ownership>();
        agentMovement       = GetComponent<FA_Movement>();
        agentCooldown       = GetComponent<FA_Cooldown>();
        agentPhysics        = GetComponent<FA_Physics>();
        agentSelection      = GetComponent<FA_Selection>();
        agentSprite         = GetComponent<FA_Sprite>();
        agentAggro          = GetComponent<FA_Aggro>();
        agentAttack         = GetComponent<FA_Attack>();

        triggerAggro        = GetComponentInChildren<FA_TriggerAggro>();
        triggerDamage       = GetComponentInChildren<FA_TriggerDamage>();
        triggerAttack       = GetComponentInChildren<FA_TriggerAttack>();

        agentMain           = GetComponent<FlockAgent>();
        rb                  = GetComponent<Rigidbody2D>();
        collider            = GetComponent<Collider2D>();
        spriteR             = GetComponentInChildren<SpriteRenderer>();
    }
}
