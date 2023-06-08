using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject chefFoule;
    public int nombreFouleActuelle;
    public int maxNombreFoule;
    public int unitétaille;
    public int tailleGrille;
    static int lastRandomNumber;

    public static SpawnManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
      //  Spawn();
    }

    void Spawn()
    {
        if (nombreFouleActuelle >= maxNombreFoule) return;

        for (int i = 0; i < maxNombreFoule; i++)
        {
            nombreFouleActuelle++;
            Instantiate(chefFoule, new Vector3(transform.position.x + (unitétaille * generateRandomNumber(-tailleGrille, tailleGrille)),
            transform.position.y + (unitétaille * generateRandomNumber(-tailleGrille, tailleGrille)), 0), transform.rotation);
        }
    }

    protected static int generateRandomNumber(int min, int max)
    {
        int result = Random.Range(min, max);

        if (result == lastRandomNumber) return generateRandomNumber(min, max);

        lastRandomNumber = result;
        return result;
    }
}
