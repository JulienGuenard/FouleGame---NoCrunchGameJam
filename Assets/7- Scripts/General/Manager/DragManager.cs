using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public static DragManager instance;

    List<GameObject> draggedUnitList = new List<GameObject>();

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public List<GameObject> GetDraggedUnitList()
    {
        return draggedUnitList;
    }

    public void Dragged(GameObject obj)
    {
        AddToDraggedUnitList(obj);
        obj.GetComponent<FA_Selection>().isDragged = true;
    }

    public void Undragged(GameObject obj)
    {
        RemoveToUndraggedUnitList(obj);
        obj.GetComponent<FA_Selection>().isDragged = false;
        obj.GetComponent<FA_Physics>().Drop();
    }

    public void AddToDraggedUnitList(GameObject obj)
    {
        if (GetDraggedUnitList().Contains(obj)) return;
        if (obj == null) return;

        draggedUnitList.Add(obj);
    }


    public void RemoveToUndraggedUnitList(GameObject obj)
    {
        if (!draggedUnitList.Contains(obj)) return;

        draggedUnitList.Remove(obj);
    }
}
