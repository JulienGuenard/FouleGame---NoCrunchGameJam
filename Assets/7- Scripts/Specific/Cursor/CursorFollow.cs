using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : CursorM
{
    public void Follow()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
