using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorRotate : Cursor
{
    public Vector3 direction;
    public Vector3 lastPos;
    public Vector3 nextPos;
    public float distance;

    bool isRotating = false;

    public void Rotate()
    {
        if (isRotating) return;

        isRotating = true;
        StartCoroutine(RotateDelay());
    }

    IEnumerator RotateDelay()
    {
        yield return new WaitForSeconds(.5f);
    //    LastRotation();
        yield return new WaitForSeconds(.5f);
        NewRotation();
    }

    void LastRotation()
    {
        if (transform.position == nextPos) return;

        lastPos = transform.position;

        if (nextPos == Vector3.zero) return;

        lastPos = nextPos;
    }

    void NewRotation()
    {
        isRotating = false;
        
        if (nextPos == transform.position) return;

        
        Vector3 nextPosTemp = transform.position;

        distance = (lastPos - nextPosTemp).magnitude;
        distance *= 1000;

          if (distance > 2000f || distance < -2000f)
           {
            Debug.Log(distance);
        direction.z = Vector2.Angle(lastPos, nextPos);
        transform.rotation = Quaternion.Euler(direction * 100);

            lastPos = nextPos;
            nextPos = nextPosTemp;
        }


    }
}
