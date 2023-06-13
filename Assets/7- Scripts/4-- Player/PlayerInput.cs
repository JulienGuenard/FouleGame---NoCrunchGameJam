using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float speed;

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb          = GetComponent<Rigidbody2D>();
        animator    = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        rb.velocity = input * speed * Time.deltaTime;
        animator.SetFloat("rotationX", input.x);
        animator.SetFloat("rotationY", input.y);
    }
}
