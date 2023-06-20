using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEvent : MonoBehaviour
{
    public void RemoveAfterDelay(float delay)
    {
        StartCoroutine(Remove(delay));
    }

    public IEnumerator Remove(float delay)
    {
        yield return new WaitForSeconds(delay);
        AttackManager.instance.BackToPullingList(this.gameObject);
    }
}
