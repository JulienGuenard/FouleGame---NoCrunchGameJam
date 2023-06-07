using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Movement : FlockAgent
{
    public Vector2 move = Vector2.zero;

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
    public void RBMove(Vector2 dir, float force)
    {
        rb.velocity = dir * force;
    }
}