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
        Passif.chef = this.gameObject;
        Aggressif.chef = this.gameObject;

        Instantiate(Passif.gameObject);
        Instantiate(Aggressif.gameObject);
    }
    public void CheckHP()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int Dmg)
    {
        this.Health -= Dmg;
        Debug.Log("TEST DMG");
    }
}
