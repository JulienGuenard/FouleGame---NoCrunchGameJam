using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject Player;
    public static Camera MainCamera;
    public GameObject machine;
    public static GameObject Cinemachine;

    public int unit�taille;
    public int tailleGrille;

    public GameObject ChefFoule;

    public int NombreFouleActuelle;
    public int maxNombreFoule;
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
 /*       if(NombreFouleActuelle < maxNombreFoule)
        {
            for (int i = 0; i < maxNombreFoule; i++)
            {
                Instantiate(ChefFoule,new Vector2(transform.position.x + (unit�taille*Random.Range(0,tailleGrille)), 
                    transform.position.y + (unit�taille * Random.Range(0, tailleGrille))),transform.rotation);
            }
        }*/

    }
}
