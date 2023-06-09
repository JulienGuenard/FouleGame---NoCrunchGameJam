using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDrop : CursorM
{
    public float forceOutput;
    float forceOutputActual;

    Vector3 directionInitial;
    Vector3 directionNext;
    Vector3 posLast;
    Vector3 postNext;
    float   distance;

    private void Start()
    {
        directionInitial = transform.up;
        forceOutputActual = 0;
    }

    public float GetForceOutputActual() { return forceOutputActual; }

    public void Rotate()
    {
        if (postNext == transform.position) return;

        posLast = postNext;
        Vector3 nextPosTemp = transform.position;

        distance = (posLast - nextPosTemp).magnitude;
        distance *= 1000;

        if (distance < 600f && distance > -600f) return;

        postNext = nextPosTemp;
        Vector3 target = postNext - posLast;

        directionNext.z = -Vector2.SignedAngle(target, directionInitial);
        transform.rotation = Quaternion.Euler(directionNext);

        forceOutputActual = forceOutput;

        StopCoroutine(StandForce());
        StartCoroutine(StandForce());
    }

    IEnumerator StandForce()
    {
        yield return new WaitForSeconds(0.2f);
        forceOutputActual = 0;
    }
}



