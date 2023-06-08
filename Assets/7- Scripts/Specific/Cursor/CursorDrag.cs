using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDrag : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 undragOffset;

    private void Update()
    {
        DragAroundCursor();
    }

    public void Drag() // Appelé par InputEvent (voir inspector)
    {
        foreach(GameObject obj in SelectableManager.instance.GetSelectableUnitList())
        {
            FA_Selection objSelection = obj.GetComponent<FA_Selection>();

            if (objSelection.isDragged)     { DragManager.instance.Undragged(obj);  continue; }
            if (objSelection.isSelectable)  { DragManager.instance.Dragged(obj);    continue; }
        }
    }

    void DragAroundCursor()
    {
        Vector3 offsetIncrement = Vector3.zero;
        foreach (GameObject obj in DragManager.instance.GetDraggedUnitList())
        {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cursorPosOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition + offset);
            Vector3 newPos = cursorPosOffset - new Vector3(0, 0, cursorPos.z) + offsetIncrement;
            offsetIncrement += new Vector3(0.2f, 0, 0);
            obj.transform.position = newPos;
        }
    }
}
