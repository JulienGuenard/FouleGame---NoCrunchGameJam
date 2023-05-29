using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bars_UI : MonoBehaviour
{
    public PlayerManager PlayerManager;

    public Slider Bar;

    public void SetBar(int compteur)
    {
       
        Bar.value = compteur;
    }

    public void SetMax(int compteur)
    {
        Bar.maxValue = compteur;
    }

}
