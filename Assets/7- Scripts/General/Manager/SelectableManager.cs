using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableManager : MonoBehaviour
{
    public static SelectableManager instance;

    List<GameObject> selectableUnitList = new List<GameObject>();

    public int selectableUnitNumberMax;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public List<GameObject> GetSelectableUnitList()
    {
        return selectableUnitList;
    }

    public void Selectable(GameObject obj)
    {
        AddToSelectableUnitList(obj);
    }

    public void Unselectable(GameObject obj)
    {
        if (DragManager.instance.GetDraggedUnitList().Contains(obj)) return;

        RemoveToSelectableUnitList(obj);
        obj.GetComponent<FA_Selection>().Unselectable();
    }

    public void AddToSelectableUnitList(GameObject obj)
    {
        if (GetSelectableUnitList().Contains(obj))              return;
        if (selectableUnitList.Count > selectableUnitNumberMax) return;
        if (obj == null)                                        return;

        selectableUnitList.Add(obj);
        SelectableAll();
    }

    void SelectableAll()
    {
        foreach(GameObject obj in selectableUnitList)
        {
            obj.GetComponent<FA_Selection>().Selectable();
        }
    }

    public void RemoveToSelectableUnitList(GameObject obj)
    {
        if (!selectableUnitList.Contains(obj)) return;
        
        selectableUnitList.Remove(obj);
    }
}
