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

    public void UpdateMovements(float rotation) { animator.SetFloat("rotation", rotation / 360); }

    public void DamagedAnimation()
    {
        animator.SetBool("isDamaged", true);
        StartCoroutine(DelayAfterNoDamageAnimation());
    }

    IEnumerator DelayAfterNoDamageAnimation()
    {
        yield return new WaitForSeconds(1f);
        EndDamagedAnimation();
    }

    public void EndDamagedAnimation()   { animator.SetBool("isDamaged", false); }

    public void ChargeAnimation()       { animator.SetBool("isCharging", true); }
    public void EndChargeAnimation()    { animator.SetBool("isCharging", false); }

    public void DragAnimation()         
    {
        if (shadow != null) shadow.SetActive(true);
        animator.SetFloat("randomDragInitial", Random.Range(0f, 2f));
        animator.SetBool("isDragged", true);
        animator.applyRootMotion = false;
    }
    public void EndDragAnimation()      
    {
        if (shadow != null) shadow.SetActive(false);
        animator.SetBool("isDragged", false);
        animator.applyRootMotion = true;
        spriteR.transform.localPosition = Vector3.zero;
    }

    public void DeadAnimation() { Instantiate(deadEffect, transform.position, Quaternion.identity); }
}
