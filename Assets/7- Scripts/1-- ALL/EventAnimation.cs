using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimation : MonoBehaviour
{
    public void DestroyThis() { Destroy(this.transform.gameObject); }
}
