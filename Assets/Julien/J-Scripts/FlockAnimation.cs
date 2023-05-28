using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAnimation : MonoBehaviour
{
    Animator animator;
    
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
}
