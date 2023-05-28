using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public Flock flockAggro;
    public Flock flockPaco;

    public int compteurAggro;
    public int compteurPaco;
    public int compteurTotal;

    public int MinSize;

    float MinCamSize;

    public float Scale;

    public CinemachineVirtualCamera cinemachine;
    // Start is called before the first frame update
    void Start()
    {
        /*cinemachine = GameManager.Cinemachine.GetComponent<CinemachineVirtualCamera>();*/
        MinCamSize = cinemachine.m_Lens.OrthographicSize;
        MinSize = flockAggro.strartingCount + flockPaco.strartingCount;
    }

    // Update is called once per frame
    void Update()
    {
        compteurAggro = flockAggro.compteur;
        compteurPaco = flockPaco.compteur;

        compteurTotal = compteurPaco + compteurAggro;

        float camsize = (MinCamSize * (compteurTotal*Scale)) / MinSize;
        if(camsize >= 0)
        {
            cinemachine.m_Lens.OrthographicSize = Mathf.Log(camsize+3,2);
        }
    }
}
