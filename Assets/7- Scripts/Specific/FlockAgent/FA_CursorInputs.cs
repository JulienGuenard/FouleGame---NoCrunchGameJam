using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_CursorInputs : FlockAgent
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

    public override void Awake()
    {
        base.Awake();
        spriteR = sprite.GetComponentInChildren<SpriteRenderer>();
        outlineMatInitial = spriteR.material;
    }

    void Update()
    {
        ForceUngrowth();

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
        Drop();
    }

    void Drag()
    {
        Vector3 cursorPos       = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cursorPosOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition + offset);
        Vector3 newPos          = cursorPosOffset - new Vector3(0, 0, cursorPos.z);
        transform.position      = newPos;
    }

    void Drop()
    {
        transform.position += undragOffset;
        DropForce();
        TryToUnhover();
    }

    void DropForce()
    {
        rb.AddForce(Cursor.instance.transform.up * forceOutput);
    }

    void ForceUngrowth()
    {
        if (rb == null) return;

        if (rb.velocity.x > 0) rb.velocity -= new Vector2(0.1f, 0);
        if (rb.velocity.y > 0) rb.velocity -= new Vector2(0, 0.1f);
        if (rb.velocity.x < 0) rb.velocity += new Vector2(0.1f, 0f);
        if (rb.velocity.y < 0) rb.velocity += new Vector2(0, 0.1f);
    }
}
