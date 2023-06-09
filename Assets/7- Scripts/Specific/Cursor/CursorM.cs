using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorM : MonoBehaviour
{
    [HideInInspector] public CursorFollow   cursorFollow;
    [HideInInspector] public CursorDrop     cursorDrop;
    [HideInInspector] public CursorTrigger  cursorTrigger;
    [HideInInspector] public CursorDrag     cursorDrag;
    [HideInInspector] public CursorSprite   cursorSprite;

    public static CursorM instance;

    public virtual void Awake()
    {
        if (instance == null) instance = this;

        cursorFollow    = GetComponent<CursorFollow>();
        cursorDrop      = GetComponent<CursorDrop>();
        cursorTrigger   = GetComponent<CursorTrigger>();
        cursorDrag      = GetComponent<CursorDrag>();
        cursorSprite    = GetComponent<CursorSprite>();
    }
}
