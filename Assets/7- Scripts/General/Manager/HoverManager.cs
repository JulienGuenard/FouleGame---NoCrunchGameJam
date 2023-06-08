using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    private List<GameObject> hoveredUnitList = new List<GameObject>();

    public static HoverManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void Hover(GameObject obj)
    {
        HoverNew(obj);
        SelectableManager.instance.Selectable(obj);
    }

    public void Unhover(GameObject obj)
    {
        SelectableManager.instance.Unselectable(obj);
        hoveredUnitList.Remove(obj);
    }

    void HoverNew(GameObject obj)
    {
       AddToHoverList(obj);
       HoverUnitAtCenter();
    }

    void AddToHoverList(GameObject obj)
    {
        if (hoveredUnitList.Contains(obj)) return;
        hoveredUnitList.Add(obj);
    }

    void HoverUnitAtCenter()
    {
        if (hoveredUnitList.Count < 1) return;
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos -= new Vector3(0, 0, cursorPos.z);
        float distanceNearest = 0;

        foreach (GameObject obj in hoveredUnitList)
        {
            Vector3 objPos = obj.transform.position - new Vector3(0, 0, obj.transform.position.z);
            float distance = (objPos - cursorPos).magnitude;

            if (SelectableManager.instance.GetSelectableUnitList().Contains(obj))   continue;
            if (distanceNearest == 0)                                               distanceNearest = distance;
            if (distance >= distanceNearest)                                        continue;

            distanceNearest = distance;
        }
    }
}
