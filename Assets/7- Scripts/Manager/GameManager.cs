using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject neutre;
    public static Flock neutreFlock;
    public static GameObject Player;
    public static Camera MainCamera;
    public static GameManager GameMa;
    public GameObject machine;
    public static GameObject Cinemachine;

    public int unitétaille;
    public int tailleGrille;

    public GameObject ChefFoule;

    public int saferayon;

    public int NombreFouleActuelle;
    public int maxNombreFoule;

    static int lastRandomNumber;
    // Start is called before the first frame update
    void Start()
    {
        neutre = GameObject.FindGameObjectWithTag("Neutre");
        neutreFlock = GameManager.neutre.GetComponent<Flock>();
        GameMa = this;
        Player = GameObject.FindGameObjectWithTag("Player");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Cinemachine = machine;
    }

    // Update is called once per frame
    void Update()
    {
        if (NombreFouleActuelle < maxNombreFoule)
        {
            for (int i = 0; i < maxNombreFoule; i++)
            {
                NombreFouleActuelle++;
                Instantiate(ChefFoule, new Vector2(transform.position.x + (unitétaille * generateRandomNumber(-tailleGrille, tailleGrille)),
                    transform.position.y + (unitétaille * generateRandomNumber(-tailleGrille, tailleGrille))), transform.rotation);
            }
        }

    }
    protected static int generateRandomNumber(int min, int max)
    {

        int result = Random.Range(min, max);

        if (result == lastRandomNumber)
        {

            return generateRandomNumber(min, max);

        }

        lastRandomNumber = result;
        return result;

    }
}
