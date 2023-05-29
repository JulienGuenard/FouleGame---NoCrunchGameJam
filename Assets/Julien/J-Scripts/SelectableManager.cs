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

    public void AddToSelectableUnitList(GameObject obj)
    {
        if (selectableUnitList.Count > selectableUnitNumberMax) return;

        selectableUnitList.Add(obj);
        HoverAll();
    }

    void HoverAll()
    {
        foreach(GameObject obj in selectableUnitList)
        {
            obj.GetComponent<MouseInteraction>().Hover();
        }
    }

    public void RemoveToSelectableUnitList(GameObject obj)
    {
        if (!selectableUnitList.Contains(obj)) return;

        selectableUnitList.Remove(obj);
        obj.GetComponent<MouseInteraction>().Unhover();
    }
}
