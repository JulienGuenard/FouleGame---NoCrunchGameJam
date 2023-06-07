using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    [HideInInspector]   public FA_Animation         agentAnimation;
    [HideInInspector]   public FA_Life              agentLife;
    [HideInInspector]   public FA_CursorInputs      agentCursorInputs;
    [HideInInspector]   public FA_Charge            agentCharge;
    [HideInInspector]   public FA_Conversion        agentConversion;
    [HideInInspector]   public FA_Ownership         agentOwnership;
    [HideInInspector]   public FA_Movement          agentMovement;
    [HideInInspector]   public FA_Cooldown          agentCooldown;
    [HideInInspector]   public Rigidbody2D          rb;
    [HideInInspector]   new public Collider2D       collider;
                        public Collider2D           AgentCollider { get { return collider; } }
                        Flock                       agentFlock;
                        public Flock                AgentFlock { get { return agentFlock; } }

    public virtual void Awake()
    {
        agentAnimation      = GetComponent<FA_Animation>();
        agentLife           = GetComponent<FA_Life>();
        agentCursorInputs   = GetComponent<FA_CursorInputs>();
        agentCharge         = GetComponent<FA_Charge>();
        agentConversion     = GetComponent<FA_Conversion>();
        agentOwnership      = GetComponent<FA_Ownership>();
        agentMovement       = GetComponent<FA_Movement>();
        agentCooldown       = GetComponent<FA_Cooldown>();

        rb                  = GetComponent<Rigidbody2D>();
        collider            = GetComponent<Collider2D>();
    }
}