using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag != "passif" && col.tag != "agressif") return;

        FA_Hover colHover = col.GetComponent<FA_Hover>();

        if (colHover.isHovered) return;

        HoverManager.instance.HoverUnit(col.gameObject);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag != "passif" && col.tag != "agressif") return;

        FA_CursorInputs colCursor = col.GetComponent<FA_CursorInputs>();

        HoverManager.instance.UnhoverUnit(col.gameObject);
    }
}
