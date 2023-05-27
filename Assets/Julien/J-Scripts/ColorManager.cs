using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;

    public Color colorNeutral;
    public Color colorHovered;
    public Color colorSelected;

    void Awake()
    {
        if (instance == null) instance = this;
    }
}
