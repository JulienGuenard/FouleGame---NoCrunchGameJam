using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDrag : MonoBehaviour
{
    public void Drag() // Appelé par InputEvent (voir inspector)
    {
        foreach(GameObject obj in SelectableManager.instance.GetSelectableUnitList())
        {
            FA_Selection    objSelection    = obj.GetComponent<FA_Selection>();
            FA_Hover        objHover        = obj.GetComponent<FA_Hover>();

            if (objSelection.isSelected)    { objSelection.Unselect();  continue; }
            if (!objHover.isHovered)        {                           continue; }

            obj.GetComponent<FA_Selection>().Select();
        }
    }
}
