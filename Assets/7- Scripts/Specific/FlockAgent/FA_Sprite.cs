using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FA_Sprite : FlockAgent
{
    public GameObject outline;
    public Vector3 outlineOffsetPos;
    private GameObject outlineInstancied;
    public Material outlineMat;
    private Material outlineMatInitial;

    public override void Awake()
    {
        base.Awake();
        outlineMatInitial = spriteR.material;
    }

    public void SelectOutline()
    {
        if (agentSprite.outlineInstancied != null) return;

        outlineInstancied = Instantiate(outline, transform.position, Quaternion.identity);
        outlineInstancied.transform.parent = spriteR.transform;
        outlineInstancied.transform.localPosition = outlineOffsetPos;
        spriteR.material = outlineMat;
    }

    public void UnselectOutline()
    {
        if (outlineInstancied == null) return;

        Destroy(outlineInstancied);
        spriteR.material = outlineMatInitial;
    }
}
