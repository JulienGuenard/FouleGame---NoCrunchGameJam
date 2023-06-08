using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag != "passif"    
        &&  col.tag != "agressif") return;

        HoverManager.instance.Hover(col.gameObject);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag != "passif"
        &&  col.tag != "agressif") return;

        HoverManager.instance.Unhover(col.gameObject);
    }
}
