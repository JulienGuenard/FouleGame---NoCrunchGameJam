using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    private SpriteRenderer spriteR;
    private Rigidbody2D rigid;

    public Sprite spriteOutlined;
    private Sprite spriteBase;

    public bool isHovered = false;
    public bool isSelected = false;

    public Vector3 offset;

    public GameObject sprite;
    public Vector3 undragOffset;
    public float delayMoveAgainAfterUndrag;
    public float forceOutput;

    void Awake()
    {
        spriteR = sprite.GetComponentInChildren<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
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
     //   rigid.AddForceAtPosition(FollowCursor.instance.transform.up * forceOutput, transform.position);
     //   StartCoroutine(MoveAgainAfterDelay());
    }

    IEnumerator MoveAgainAfterDelay()
    {
        GetComponent<FlockAgent>().movementIsEnabled = false;
        yield return new WaitForSeconds(delayMoveAgainAfterUndrag);
        GetComponent<FlockAgent>().movementIsEnabled = true;
    }
}
