using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Movement : FlockAgent
{
    public Vector2 velocity = Vector2.zero;

    public void Move(Vector2 velocity)
    {
        if (agentAttack.isAttacking) return;

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
