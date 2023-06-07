using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Drag : FlockAgent
{
    public Vector3 offset;
    public Vector3 undragOffset;

    void Update()
    {
        if (agentSelection.isSelected) Drag();
    }

    public void Drag()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cursorPosOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition + offset);
        Vector3 newPos = cursorPosOffset - new Vector3(0, 0, cursorPos.z);
        transform.position = newPos;
    }

    public void Drop()
    {
        transform.position += undragOffset;
        agentPhysics.DropForce();
        HoverManager.instance.UnhoverUnit(this.gameObject);
    }
}
