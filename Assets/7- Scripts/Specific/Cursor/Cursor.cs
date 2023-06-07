using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [HideInInspector] public CursorFollow cursorFollow;
    [HideInInspector] public CursorRotate cursorRotate;

    public virtual void Awake()
    {
        cursorFollow = GetComponent<CursorFollow>();
        cursorRotate = GetComponent<CursorRotate>();
    }
}
