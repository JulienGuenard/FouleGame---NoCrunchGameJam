using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Selection : FlockAgent
{
    public bool isSelectable = false;
    public bool isDragged = false;

    public void Selectable()
    {
        isSelectable = true;
        agentSprite.SelectOutline();
    }

    public void Unselectable()
    {
        isSelectable = false;
        agentSprite.UnselectOutline();
    }
}
