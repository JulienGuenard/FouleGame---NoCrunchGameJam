using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorRotate : Cursor
{
    Vector3 direction;
    Vector3 lastPos;
    Vector3 nextPos;
    Vector3 directionInitial;
    float distance;

    private void Start()
    {
        directionInitial = transform.up;
    }

    public void Rotate()
    {
        if (nextPos == transform.position) return;

        lastPos = nextPos;
        Vector3 nextPosTemp = transform.position;

        distance = (lastPos - nextPosTemp).magnitude;
        distance *= 1000;

        if (distance < 800f && distance > -800f) return;

        nextPos = nextPosTemp;
        Vector3 target = nextPos - lastPos;

        direction.z = -Vector2.SignedAngle(target, directionInitial);
        transform.rotation = Quaternion.Euler(direction);
    }
}



