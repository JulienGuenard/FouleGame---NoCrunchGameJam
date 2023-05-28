using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    private SpriteRenderer spriteR;

    public Sprite spriteOutlined;
    private Sprite spriteBase;

    public bool isHovered = false;
    public bool isSelected = false;

    public Vector3 offset;

    public GameObject sprite;
    public Vector3 undragOffset;

    public float forceOutput;

    void Awake()
    {
        spriteR = sprite.GetComponentInChildren<SpriteRenderer>();
        spriteBase = spriteR.sprite;
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("hover");
        if (col.tag != "Cursor") return;
        if (isHovered) return;
        if (isSelected) return;

        TryToHover();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag != "Cursor") return;
        if (isSelected) return;
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
        spriteR.sprite = spriteOutlined;
    }

    public void Unhover()
    {
        isHovered = false;
        spriteR.sprite = spriteBase;
    }

    void Select()
    {
        isSelected = true;
        isHovered = false;
    }

    void Unselect()
    {
        isSelected = false;
        spriteR.sprite = spriteBase;
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
