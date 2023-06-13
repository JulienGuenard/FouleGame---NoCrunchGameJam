using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayerAutoSort : MonoBehaviour
{
    [SerializeField]    private int layerOffset;
    [SerializeField]    bool lookAtParent;
    [SerializeField]    bool changeZ;

                        private SpriteRenderer spriteR;

    void Awake()
    {
        spriteR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (lookAtParent)   spriteR.sortingOrder = Mathf.RoundToInt(-transform.parent.position.y*50) + layerOffset;
        else                spriteR.sortingOrder = Mathf.RoundToInt(-transform.position.y * 50) + layerOffset;

        if (changeZ)        transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y, -spriteR.sortingOrder);
    }
}
