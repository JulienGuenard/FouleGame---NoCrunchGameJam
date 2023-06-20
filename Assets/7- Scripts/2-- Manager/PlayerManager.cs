using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerManager : MonoBehaviour
{


    [Header("Agressif Player Group")]
    public Flock    agressifFlock;
    public Bars_UI  agressifBar;
    public int      compteurAgressif;

    [Header("Passif Player Group")]
    public Flock    passifFlock;
    public Bars_UI  passifBar;
    public int      compteurPassif;

    [Header("Total Player Group")]
    public int      compteurTotal;

    [Header("Camera")]
    public CinemachineVirtualCamera cinemachine;
    public int      MinSize;
    public float    camScale;
    float           MinCamSize;
    
    public static PlayerManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        MinCamSize = cinemachine.m_Lens.OrthographicSize;
        MinSize = agressifFlock.FSpawn.startingCount + passifFlock.FSpawn.startingCount;
    }

    void Update()
    {
        UpdatePlayerAgentCount();
        SetBars();
        AdjustCamSize();
    }

    void UpdatePlayerAgentCount()
    {
        compteurAgressif    = agressifFlock.FBehaviour.agents.Count;
        compteurPassif      = passifFlock.FBehaviour.agents.Count;

        compteurTotal       = compteurPassif + compteurAgressif;
    }

    void AdjustCamSize()
    {
        float camsize = (MinCamSize * (compteurTotal * camScale)) / MinSize;

        if (camsize < 0) return;

        cinemachine.m_Lens.OrthographicSize = Mathf.Log(camsize + 3, 2);
    }

    public void SetBars()
    {
        if (compteurTotal == 0) return;

        float   fill = (float)(compteurTotal - compteurAgressif) / compteurTotal;
                passifBar.SetBar(fill);
                fill = (float)(compteurTotal - compteurPassif) / compteurTotal;
                agressifBar.SetBar(fill);
    }

    public void CheckGameOver(int compteur)
    {
        if (compteur != 0) return;

        MenuManager.instance.LoadScene("MenuGameOver");
    }
}
