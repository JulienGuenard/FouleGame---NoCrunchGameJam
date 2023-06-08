using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Selection : FlockAgent
{
    public bool isSelected = false;

    public void Selectable()
    {
        agentHover.isHovered = true;
        agentSprite.SelectOutline();
    }

    public void Unselectable()
    {
        agentHover.isHovered = false;
        agentSprite.UnselectOutline();
    }

    public void Select()
    {
        isSelected = true;
        agentHover.isHovered = false;
    }

    public void Unselect()
    {
        isSelected = false;
        agentDrag.Drop();
    }
}
