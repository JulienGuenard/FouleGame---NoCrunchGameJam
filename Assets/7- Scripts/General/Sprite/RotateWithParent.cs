using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithParent : MonoBehaviour
{
    public bool canRotate = true;

    void Update()
    {
        Rotate();
    }


    void Rotate()
    {
        transform.localRotation = Quaternion.Euler(-transform.parent.rotation.eulerAngles);
    }
}
