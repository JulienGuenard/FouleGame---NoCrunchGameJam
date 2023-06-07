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
        HoverNewUnit();
    }

    void HoverNewUnit()
    {
        lastHoveredUnit = hoveredUnit;
        SetHoveredUnit(null);

        if (hoveredUnitList.Count >= 1) HoverUnitAtCenter();
    }

    public void HoverUnitAtCenter()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0,0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
        float distanceNearest = 0;

        foreach (GameObject obj in hoveredUnitList)
        {
            Vector3 objPos = obj.transform.position - new Vector3(0, 0, obj.transform.position.z);
            float distance = (objPos - cursorPos).magnitude;

            if (SelectableManager.instance.GetSelectableUnitList().Contains(obj)) continue; // continue passe l'itération, break passe la boucle entière (comme return avec les fonctions)

            if (distanceNearest == 0) distanceNearest = distance;

            if (hoveredUnit == null || distance < distanceNearest)
            {
                distanceNearest = distance;
                SetHoveredUnit(obj);
            }
        }
    }

    void SetHoveredUnit(GameObject obj) { hoveredUnit = obj; }

    public void HoverUnit(GameObject obj)
    {
        HoverNewUnit();
        Hover();

        if (hoveredUnitList.Contains(obj)) return;

        hoveredUnitList.Add(obj);
    }

    public void UnhoverUnit(GameObject obj)
    {
        Unhover(obj);
        hoveredUnitList.Remove(obj);
    }

    void Hover()
    {
        if (hoveredUnit == null) return;
        if (SelectableManager.instance.GetSelectableUnitList().Contains(hoveredUnit)) return;

        SelectableManager.instance.AddToSelectableUnitList(hoveredUnit);
    }

   void Unhover(GameObject obj)
    {
        SelectableManager.instance.RemoveToSelectableUnitList(obj);
    }
}
