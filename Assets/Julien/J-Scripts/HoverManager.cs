using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    public static HoverManager instance;

    public GameObject hoveredUnit;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        
    }
}
