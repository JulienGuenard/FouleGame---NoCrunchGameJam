using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorM : MonoBehaviour
{
    [HideInInspector] public CursorFollow   cursorFollow;
    [HideInInspector] public CursorRotate   cursorRotate;
    [HideInInspector] public CursorTrigger  cursorTrigger;
    [HideInInspector] public CursorDrag     cursorDrag;
    [HideInInspector] public CursorSprite   cursorSprite;

    public static CursorM instance;

    public virtual void Awake()
    {
        if (instance == null) instance = this;

        cursorFollow    = GetComponent<CursorFollow>();
        cursorRotate    = GetComponent<CursorRotate>();
        cursorTrigger   = GetComponent<CursorTrigger>();
        cursorDrag      = GetComponent<CursorDrag>();
        cursorSprite    = GetComponent<CursorSprite>();
    }
}
