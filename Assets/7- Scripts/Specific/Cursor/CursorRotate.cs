using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorRotate : Cursor
{
    Vector3 directionInitial;
    Vector3 directionNext;
    Vector3 posLast;
    Vector3 postNext;
    float   distance;

    private void Start()
    {
        directionInitial = transform.up;
    }

    public void Rotate()
    {
        if (postNext == transform.position) return;

        posLast = postNext;
        Vector3 nextPosTemp = transform.position;

        distance = (posLast - nextPosTemp).magnitude;
        distance *= 1000;

        if (distance < 800f && distance > -800f) return;

        postNext = nextPosTemp;
        Vector3 target = postNext - posLast;

        directionNext.z = -Vector2.SignedAngle(target, directionInitial);
        transform.rotation = Quaternion.Euler(directionNext);
    }
}



