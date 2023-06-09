using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDrag : CursorM
{
    public AnimationCurve dragIncrementCurveX;
    public AnimationCurve dragIncrementCurveY;
    public float dragIncrementEcartX;
    public float dragIncrementEcartY;
    public float dragIncrementEcart;
    public float curveIncrement;
    public float iX_Increment;
    public float iY_Increment;

    private void Update()
    {
        DragAroundCursor();
    }

    public void Drag() // Appelé par InputEvent (voir inspector)
    {
        List<GameObject> list = new List<GameObject>();
        list.AddRange(DragManager.instance.GetDraggedUnitList());

        foreach (GameObject obj in list)
        {
            FA_Selection objSelection = obj.GetComponent<FA_Selection>();

            if (objSelection.isDragged) { DragManager.instance.Undragged(obj); continue; }
        }

        list.Clear();
        list.AddRange(SelectableManager.instance.GetSelectableUnitList());

        foreach (GameObject obj in list)
        {
            FA_Selection objSelection = obj.GetComponent<FA_Selection>();

            if (objSelection.isSelectable)  { DragManager.instance.Dragged(obj);    continue; }
        }
    }

    void DragAroundCursor()
    {
        Vector3 offsetIncrement = Vector3.zero;
        float iX = 0;
        float iY = 0;
        foreach (GameObject obj in DragManager.instance.GetDraggedUnitList())
        {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cursorPosOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPos = cursorPosOffset - new Vector3(0, 0, cursorPos.z) + offsetIncrement;
            offsetIncrement = new Vector3((dragIncrementCurveX.Evaluate(iX) -0.5f) * dragIncrementEcartX, (dragIncrementCurveY.Evaluate(iY) -0.5f) * dragIncrementEcartY, 0) * dragIncrementEcart;
            if (obj == null) continue;
            obj.transform.position = newPos;
            iX += iX_Increment;
            iY += iY_Increment;
            obj.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
