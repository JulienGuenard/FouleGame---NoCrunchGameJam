using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [HideInInspector] public CursorFollow cursorFollow;
    [HideInInspector] public CursorRotate cursorRotate;

    public static Cursor instance;

    public virtual void Awake()
    {
        if (instance == null) instance = this;

        cursorFollow = GetComponent<CursorFollow>();
        cursorRotate = GetComponent<CursorRotate>();
    }
}
