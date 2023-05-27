using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAutre : MonoBehaviour
{
    private float absorbRadius;
    public bool autreEntered;

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.CircleCast(transform.position, absorbRadius, transform.up))
        {
            autreEntered = true;
        }
    }
}
