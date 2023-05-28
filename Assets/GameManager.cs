using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject Player;
    public static Camera MainCamera;
    public GameObject machine;
    public static GameObject Cinemachine;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Cinemachine = machine;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
