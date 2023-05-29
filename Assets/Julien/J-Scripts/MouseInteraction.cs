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

    public GameObject outline;
    public Vector3 outlineOffsetPos;
    private GameObject outlineInstancied;
    public Material outlineMat;
    private Material outlineMatInitial;

    void Awake()
    {
        spriteR = sprite.GetComponentInChildren<SpriteRenderer>();
        outlineMatInitial = spriteR.material;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isHovered && !isSelected)
            {
                Select();
            }
            else if (isSelected)
            {
                Unselect();
            }
        }

        if (isSelected)
        {
            Drag();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag != "Cursor") return;
        if (isHovered) return;

        TryToHover();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag != "Cursor") return;
        TryToUnhover();
    }

    void TryToHover()
    {
        HoverManager.instance.HoverUnit(this.gameObject);
    }

    public void TryToUnhover()
    {
        HoverManager.instance.UnhoverUnit(this.gameObject);
    }

    public void Hover()
    {
        isHovered = true;

        if (outlineInstancied != null) return;

        outlineInstancied = Instantiate(outline, transform.position + outlineOffsetPos, Quaternion.identity);
        outlineInstancied.transform.parent = spriteR.transform;
        spriteR.material = outlineMat;
    }

    public void Unhover()
    {
        isHovered = false;

        if (outlineInstancied == null) return;

        Destroy(outlineInstancied);
        spriteR.material = outlineMatInitial;
    }

    void Select()
    {
        isSelected = true;
        isHovered = false;
    }

    void Unselect()
    {
        isSelected = false;
        Undrag();
    }

    void Drag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + offset) - new Vector3(0, 0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
    }

    void Undrag()
    {
        transform.position += undragOffset;
        TryToUnhover();
    }
}
