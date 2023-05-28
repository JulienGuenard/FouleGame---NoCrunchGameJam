using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef_instance : MonoBehaviour
{
    public Flock Passif;
    public Flock Aggressif;

    private void Start()
    {
        Passif.chef = this.gameObject;
        Aggressif.chef = this.gameObject;

        Instantiate(Passif.gameObject);
        Instantiate(Aggressif.gameObject);
    }
}
