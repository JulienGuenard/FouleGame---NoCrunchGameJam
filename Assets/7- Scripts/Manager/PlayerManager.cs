using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerManager : MonoBehaviour
{
    public Flock flockAggro;
    public Flock flockPaco;

    public Bars_UI AgressifBar;
    public Bars_UI PassifBar;
  

    public int compteurAggro;
    public int compteurPaco;
    public int compteurTotal;

    public int MinSize;

    float MinCamSize;

    public float Scale;

    public CinemachineVirtualCamera cinemachine;

    public static PlayerManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        /*cinemachine = GameManager.Cinemachine.GetComponent<CinemachineVirtualCamera>();*/
        MinCamSize = cinemachine.m_Lens.OrthographicSize;
        MinSize = flockAggro.strartingCount + flockPaco.strartingCount;
     
    }

    void Update()
    {
        compteurAggro = flockAggro.compteur;
        compteurPaco = flockPaco.compteur;
     
        compteurTotal = compteurPaco + compteurAggro;
        SetBars();

        float camsize = (MinCamSize * (compteurTotal*Scale)) / MinSize;
        if(camsize >= 0)
        {
            cinemachine.m_Lens.OrthographicSize = Mathf.Log(camsize+3,2);
        }
    }

    public void SetBars()
    {
        PassifBar.SetMax(compteurTotal);
        AgressifBar.SetMax(compteurTotal);
        AgressifBar.SetBar(compteurAggro);
        PassifBar.SetBar(compteurPaco);
    }

    public void CHeckGameOver(int compteur)
    {
        if(compteur == 0) 
        {
            MenuManager.Instance.LoadScene("MenuGameOver");
        }
    }

   
}
