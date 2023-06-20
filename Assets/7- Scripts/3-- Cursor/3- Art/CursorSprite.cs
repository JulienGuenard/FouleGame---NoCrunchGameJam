using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSprite : CursorM
{
    public Texture2D idleTexture;
    public Texture2D selectableTexture;
    public Texture2D dragTexture;

    public void ChangeCursorSprite()
    {
        if (DragManager.instance.GetDraggedUnitList().Count != 0)           
        {   
            Cursor.SetCursor(dragTexture, Vector2.zero, CursorMode.Auto); 
            return; 
        }

        if (SelectableManager.instance.GetSelectableUnitList().Count != 0)  
        {   
            Cursor.SetCursor(selectableTexture, Vector2.zero, CursorMode.Auto); 
            return; 
        }
                                                                                
            Cursor.SetCursor(idleTexture, Vector2.zero, CursorMode.Auto);
    }
}
