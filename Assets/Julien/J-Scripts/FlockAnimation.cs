using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAnimation : MonoBehaviour
{
    Animator animator;

    public GameObject deadEffect;
    
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
            UpdateMovements(transform.rotation.eulerAngles.z);
    }

    public void UpdateMovements(float rotation)
    {
        animator.SetFloat("rotation", rotation / 360);
    }

    public void DamagedAnimation()
    {
        animator.SetBool("isDamaged", true);
        StartCoroutine(DelayAfterNoDamageAnimation());
    }

    public void DeadAnimation()
    {
        Instantiate(deadEffect, transform.position, Quaternion.identity);
    }

    public void ChargeAnimation()
    {
        animator.SetBool("isCharging", true);
    }

    public void EndChargeAnimation()
    {
        animator.SetBool("isCharging", false);
    }

    public void EndDamagedAnimation()
    {
        animator.SetBool("isDamaged", false);
    }


    IEnumerator DelayAfterNoDamageAnimation()
    {
        yield return new WaitForSeconds(1f);
        EndDamagedAnimation();
    }
}
