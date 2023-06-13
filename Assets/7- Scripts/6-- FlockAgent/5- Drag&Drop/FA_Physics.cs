using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Physics : FlockAgent
{
    private void Update()
    {
        ForceUngrowth();
    }

    public void Drop()
    {
        rb.AddForce(CursorM.instance.transform.up * CursorM.instance.cursorDrop.GetForceOutputActual());
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
