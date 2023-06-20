using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public AnimationCurve dragIncrementCurveX;
    public AnimationCurve dragIncrementCurveY;
    public float dragIncrementEcartX;
    public float dragIncrementEcartY;
    public float dragIncrementEcart;
    public float curveIncrement;
    public float iX_Increment;
    public float iY_Increment;

    List<GameObject> draggedUnitList = new List<GameObject>();

    public static DragManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public List<GameObject> GetDraggedUnitList()
    {
        return draggedUnitList;
    }

    ////////////////////////////////// UPDATE //////////////////////////////////////////////
    public void DraggedUnitsFollowCursor() // Appelé par UpdateEvent (voir inspector)
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 offsetIncrement = Vector3.zero;
        float iX = 0, iY = 0;

        foreach (GameObject obj in draggedUnitList)
        {
            if (obj == null) continue;

            Vector3 newPos = cursorPos - new Vector3(0, 0, cursorPos.z) + offsetIncrement;

            float posX = (dragIncrementCurveX.Evaluate(iX) - 0.5f) * dragIncrementEcartX;
            float posY = (dragIncrementCurveY.Evaluate(iY) - 0.5f) * dragIncrementEcartY;
            offsetIncrement = new Vector3(posX, posY, 0) * dragIncrementEcart;

            obj.transform.position = newPos;
            obj.transform.localRotation = Quaternion.Euler(0, 0, 0);

            iX += iX_Increment;
            iY += iY_Increment;
        }
    }

    ////////////////////////////////// LEFT CLICK //////////////////////////////////////////////
    public void Drag() // Appelé par InputEvent du Curseur (voir inspector)
    {
        List<GameObject>    list = new List<GameObject>();
                            list.AddRange(draggedUnitList);
                            TryToUndrag(list);
                            list.Clear();
                            list.AddRange(SelectableManager.instance.GetSelectableUnitList());
                            TryToDrag(list);
    }

    void TryToUndrag(List<GameObject> list)
    {
        foreach (GameObject obj in list)
        {
            if (obj == null) continue;
            FA_Selection objSelection = obj.GetComponent<FA_Selection>();
            if (objSelection.isDragged) UndragUnit(obj);
        }
    }

    void TryToDrag(List<GameObject> list)
    {
        foreach (GameObject obj in list)
        {
            FA_Selection objSelection = obj.GetComponent<FA_Selection>();
            if (objSelection.isSelectable) DragUnit(obj);
        }
    }

   void DragUnit(GameObject obj)
    {
        SelectableManager.instance.Unselectable(obj);
        AddToDraggedUnitList(obj);
        obj.GetComponent<FA_Selection>().isDragged = true;
        obj.GetComponent<FA_Animation>().DragStart();
    }

    void UndragUnit(GameObject obj)
    {
        RemoveToUndraggedUnitList(obj);
        obj.GetComponent<FA_Selection>().isDragged = false;
        obj.GetComponent<FA_Animation>().DragEnd();
        obj.GetComponent<FA_Physics>().Drop();
    }

    void AddToDraggedUnitList(GameObject obj)
    {
        if (obj == null)                        return;
        if (GetDraggedUnitList().Contains(obj)) return;

        draggedUnitList.Add(obj);
    }

    void RemoveToUndraggedUnitList(GameObject obj)
    {
        if (!draggedUnitList.Contains(obj))     return;

        draggedUnitList.Remove(obj);
    }
}
