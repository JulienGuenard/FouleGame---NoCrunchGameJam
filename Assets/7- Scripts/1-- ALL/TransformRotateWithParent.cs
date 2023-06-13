using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRotateWithParent : MonoBehaviour
{
    void Update()
    {
        Rotate();
    }

    void Rotate() { transform.localRotation = Quaternion.Euler(-transform.parent.rotation.eulerAngles); }
}
