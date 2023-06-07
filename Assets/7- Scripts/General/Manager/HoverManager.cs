using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    public static HoverManager instance;

    private GameObject hoveredUnit;
    private GameObject lastHoveredUnit;
    private List<GameObject> hoveredUnitList = new List<GameObject>();

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void HoverNewUnit() // Appelé par UpdateEvent (voir inspector)
    {
        lastHoveredUnit = hoveredUnit;
        hoveredUnit = null;

        if (hoveredUnitList.Count < 1) return;

        HoverUnitAtCenter();
    }

    public void HoverUnitAtCenter()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos -= new Vector3(0, 0, cursorPos.z);
        float distanceNearest = 0;

        foreach (GameObject obj in hoveredUnitList)
        {
            Vector3 objPos = obj.transform.position - new Vector3(0, 0, obj.transform.position.z);
            float distance = (objPos - cursorPos).magnitude;

            if (SelectableManager.instance.GetSelectableUnitList().Contains(obj))   continue;
            if (distanceNearest == 0)                                               distanceNearest = distance;
            if (hoveredUnit != null && distance >= distanceNearest)                 continue;

            distanceNearest = distance;
            hoveredUnit = obj;
        }
    }

    public void HoverUnit(GameObject obj)
    {
        HoverNewUnit();
        SelectableManager.instance.AddToSelectableUnitList(hoveredUnit);

        if (hoveredUnitList.Contains(obj)) return;

        hoveredUnitList.Add(obj);
    }

    public void UnhoverUnit(GameObject obj)
    {
        SelectableManager.instance.RemoveToSelectableUnitList(obj);
        hoveredUnitList.Remove(obj);
    }
}
