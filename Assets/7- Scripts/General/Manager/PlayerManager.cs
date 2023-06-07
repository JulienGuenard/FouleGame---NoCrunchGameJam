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
    public float camScale;
    float MinCamSize;
    
    public CinemachineVirtualCamera cinemachine;

    public static PlayerManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        MinCamSize = cinemachine.m_Lens.OrthographicSize;
        MinSize = flockAggro.FSpawn.startingCount + flockPaco.FSpawn.startingCount;
    }

    void Update()
    {
        UpdatePlayerAgentCount();
        SetBars();
        AdjustCamSize();
    }

    void UpdatePlayerAgentCount()
    {
        compteurAggro = flockAggro.FBehaviour.agents.Count;
        compteurPaco = flockPaco.FBehaviour.agents.Count;

        compteurTotal = compteurPaco + compteurAggro;
    }

    void AdjustCamSize()
    {
        float camsize = (MinCamSize * (compteurTotal * camScale)) / MinSize;

        if (camsize < 0) return;

        cinemachine.m_Lens.OrthographicSize = Mathf.Log(camsize + 3, 2);
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
        if (compteur != 0) return;

        MenuManager.Instance.LoadScene("MenuGameOver");
    }

   
}
