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

    private void Update()
    {
        lastHoveredUnit = hoveredUnit;
        if (hoveredUnitList.Count >= 2) HoverUnitAtCenter();
        if (hoveredUnitList.Count == 1) hoveredUnit = hoveredUnitList[0];
        if (hoveredUnitList.Count == 0)
        {
            hoveredUnit = null;
            return;
        }
        if (lastHoveredUnit != null) Unhover(lastHoveredUnit);
        Hover();
    }

    public GameObject GetHoveredUnit() { return hoveredUnit; }

    public void HoverUnit(GameObject obj)
    {
        if (hoveredUnitList.Contains(obj)) return;

        hoveredUnitList.Add(obj);
    }

    public void UnhoverUnit(GameObject obj)
    {
        if (!hoveredUnitList.Contains(obj)) return;

        Unhover(obj);
        hoveredUnitList.Remove(obj);
    }

    public void HoverUnitAtCenter()
    {
        GameObject unit;
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0,0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
        float distanceNearest = 0;

        foreach (GameObject obj in hoveredUnitList)
        {
            Vector3 objPos = obj.transform.position - new Vector3(0, 0, obj.transform.position.z);
            float distance = (objPos - cursorPos).magnitude;
            if (distanceNearest == 0) distanceNearest = distance;

            if (hoveredUnit == null)
            {
                distanceNearest = distance;
                hoveredUnit = obj;
            }
            else if (distance < distanceNearest)
            {
                distanceNearest = distance;
                hoveredUnit = obj;
            }
        }
    }

    void Hover()
    {
        hoveredUnit.GetComponent<MouseInteraction>().Hover();
    }

    void Unhover(GameObject obj)
    {
        obj.GetComponent<MouseInteraction>().Unhover();
    }
}
