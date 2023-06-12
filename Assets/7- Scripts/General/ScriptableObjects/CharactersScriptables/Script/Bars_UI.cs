using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bars_UI : MonoBehaviour
{
    Image slider;

    private void Awake()
    {
        slider = GetComponent<Image>();
    }

    public void SetBar(float compteur)
    {
        Debug.Log(compteur);
        slider.fillAmount = compteur;
    }
}
