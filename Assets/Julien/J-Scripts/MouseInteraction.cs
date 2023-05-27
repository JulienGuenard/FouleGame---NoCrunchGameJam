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
    public GameObject shadow;
    public Vector3 shadowDragOffset;
    private Vector3 shadowDragOffsetInitial;
    public Vector3 undragOffset;

    void Awake()
    {
        spriteR = sprite.GetComponent<SpriteRenderer>();

        shadowDragOffsetInitial = shadow.transform.localPosition;
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

    private void OnMouseOver()
    {
        if (isHovered) return;
        if (isSelected) return;

        Hover();
    }

    private void OnMouseExit()
    {
        if (isSelected) return;
        Unhover();
    }

    void Hover()
    {
        isHovered = true;
        spriteR.color = ColorManager.instance.colorHovered;
    }

    void Unhover()
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
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + offset) - new Vector3(0,0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
        shadow.transform.localPosition = shadowDragOffset;
    }

    void Undrag()
    {
        transform.position += undragOffset;
        shadow.transform.localPosition = shadowDragOffsetInitial;
    }
}
