using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableManager : MonoBehaviour
{
    public static SelectableManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        
    }
}
