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
        if (obj.GetComponent<FA_Ownership>() == null) return;
        if (!obj.GetComponent<FA_Ownership>().isPlayer)             return;
        if (GetSelectableUnitList().Contains(obj))                  return;
        if (DragManager.instance.GetDraggedUnitList().Count != 0)   return;
        if (selectableUnitList.Count > selectableUnitNumberMax)     return;
        if (obj == null)                                            return;

        AddToSelectableUnitList(obj);
        SelectableAll();
    }

    public void Unselectable(GameObject obj)
    {
        if (obj == null) return;
        if (obj.GetComponent<FA_Selection>() == null) return;

        RemoveToSelectableUnitList(obj);
        obj.GetComponent<FA_Selection>().Unselectable();
    }

    public void AddToSelectableUnitList(GameObject obj)
    {
        selectableUnitList.Add(obj);
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
