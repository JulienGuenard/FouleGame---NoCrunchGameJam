using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    private SpriteRenderer spriteR;

    public bool isHovered = false;
    public bool isSelected = false;

    public Vector3 offset;

    public GameObject sprite;
    public Vector3 undragOffset;

    public float forceOutput;

    void Awake()
    {
        spriteR = sprite.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isHovered && !isSelected)
            {
                Select();
            }
            else if (!isHovered && isSelected)
            {
                Unselect();
            }
        }

        if (isSelected)
        {
            Drag();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Cursor") return;
        if (isHovered) return;
        if (isSelected) return;

        Hover();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag != "Cursor") return;
        if (isSelected) return;
        Unhover();
    }

    void Hover()
    {
        isHovered = true;
        spriteR.color = ColorManager.instance.colorHovered;

        if (HoverManager.instance.hoveredUnit != null && HoverManager.instance.hoveredUnit != this.gameObject) HoverManager.instance.hoveredUnit.GetComponent<MouseInteraction>().Unhover();

        HoverManager.instance.hoveredUnit = this.gameObject;
    }

    public void Unhover()
    {
        isHovered = false;
        spriteR.color = ColorManager.instance.colorNeutral;
    }

    void Select()
    {
        isSelected = true;
        isHovered = false;
        spriteR.color = ColorManager.instance.colorSelected;
    }

    void Unselect()
    {
        isSelected = false;
        spriteR.color = ColorManager.instance.colorNeutral;
        Undrag();
    }

    void Drag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + offset) - new Vector3(0, 0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
    }

    void Undrag()
    {
        transform.position += undragOffset;
    }
}
