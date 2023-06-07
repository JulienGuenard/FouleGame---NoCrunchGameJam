using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Physics : FlockAgent
{
    public float forceOutput;

    private void Update()
    {
        ForceUngrowth();
    }

    public void DropForce()
    {
        rb.AddForce(Cursor.instance.transform.up * forceOutput);
    }

    void ForceUngrowth()
    {
        if (rb == null) return;

        if (rb.velocity.x > 0) rb.velocity -= new Vector2(0.1f, 0);
        if (rb.velocity.y > 0) rb.velocity -= new Vector2(0, 0.1f);
        if (rb.velocity.x < 0) rb.velocity += new Vector2(0.1f, 0f);
        if (rb.velocity.y < 0) rb.velocity += new Vector2(0, 0.1f);
    }
}
