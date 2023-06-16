using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float delay;

    void Start()
    {
       // StartCoroutine(DestroyAfterDelay());
    }

    public IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        AttackManager.instance.BackToPullingList(this.gameObject);
    }
}
