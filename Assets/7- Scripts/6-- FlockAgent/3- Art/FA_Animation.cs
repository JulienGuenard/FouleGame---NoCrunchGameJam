using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FA_Animation : FlockAgent
{
    Animator animator;

    public GameObject deadEffect;
    public GameObject shadow;
    
    public override void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
        if (shadow != null) shadow.SetActive(false);
    }

    private void Update()
    {
        UpdateMovements(transform.rotation.eulerAngles.z);
    }

    ///     // Move
    public void UpdateMovements(float rotation)     { animator.SetFloat("rotation", rotation / 360); }

    ///     // Damaged
    public void DamagedStart()                      { spriteR.color = Color.red; StartCoroutine(DelayAfterNoDamageAnimation()); }
    IEnumerator DelayAfterNoDamageAnimation()       { yield return new WaitForSeconds(1f); DamagedEnd(); }
    public void DamagedEnd()                        { spriteR.color = Color.white; }

    ///     // Charge Preparation
    public void ChargePrepStart()  { animator.SetBool("inPrepToCharge", true);  }
    public void ChargePrepEnd()                     { animator.SetBool("inPrepToCharge", false); }

    ///     // Charge
    public void ChargeStart()                       { animator.SetBool("isCharging", true); }
    public void ChargeEnd()                         { animator.SetBool("isCharging", false); }

    ///     // Attack Preparation
    public void AttackPrepStart()                   { animator.SetBool("inPrepToAttack", true);  }
    public void AttackPrepEnd()                     { animator.SetBool("inPrepToAttack", false); }

    ///     // Attack
    public void AttackStart(float attackSpeed)      { animator.SetBool("isAttacking", true); animator.SetFloat("attackSpeed", attackSpeed); }
    public void AttackEnd()                         { animator.SetBool("isAttacking", false); }

    ///     // Drag
    public void DragStart()         
    {
        if (shadow != null) shadow.SetActive(true);
        animator.SetFloat("randomDragInitial", Random.Range(0f, 2f));
        animator.SetBool("isDragged", true);
        animator.applyRootMotion = false;
    }

    public void DragEnd()      
    {
        if (shadow != null) shadow.SetActive(false);
        animator.SetBool("isDragged", false);
        animator.applyRootMotion = true;
        spriteR.transform.localPosition = Vector3.zero;
    }

    ///     // Dead
    public void DeadAnimation() { Instantiate(deadEffect, transform.position, Quaternion.identity); }
}
