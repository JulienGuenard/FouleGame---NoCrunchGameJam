using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef_instance : MonoBehaviour
{
    public Flock Passif;
    public Flock Aggressif;

    public int Health;

    GameManager gamema;

    private void Start()
    {
        gamema = GameManager.GameMa;
    /*   if (Passif.FOwnership.chef == null)      Passif.FOwnership.chef = this.gameObject;
       if (Aggressif.FOwnership.chef == null)   Aggressif.FOwnership.chef = this.gameObject;

        Instantiate(Passif.gameObject);
        Instantiate(Aggressif.gameObject);*/
    }
    private void Update()
    {
        Health = GetComponent<FlockAgent>().Health;
        CheckHP();
    }
    public void CheckHP()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            gamema.NombreFouleActuelle--;
        }
    }

}
