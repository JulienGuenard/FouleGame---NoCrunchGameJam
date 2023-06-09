using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSprite : CursorM
{
    public Texture2D cursorIdle;
    public Texture2D cursorSelectable;
    public Texture2D cursorDrag;

    private void Update()
    {
        ChangeCursorSprite();
    }

    void ChangeCursorSprite()
    {
        if (DragManager.instance.GetDraggedUnitList().Count != 0)           {   Cursor.SetCursor(cursorDrag, Vector2.zero, CursorMode.Auto); return; }
        if (SelectableManager.instance.GetSelectableUnitList().Count != 0)  {   Cursor.SetCursor(cursorSelectable, Vector2.zero, CursorMode.Auto); return; }
                                                                                Cursor.SetCursor(cursorIdle, Vector2.zero, CursorMode.Auto);
    }
}
