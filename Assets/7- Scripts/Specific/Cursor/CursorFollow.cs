using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : Cursor
{
    public void Follow()
    {
        transform.position = GameManager.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
