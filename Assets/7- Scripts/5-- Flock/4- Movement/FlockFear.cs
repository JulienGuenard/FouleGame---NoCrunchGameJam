using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockFear : Flock
{
    public Vector2 Fear(Vector2 Direction)
    {
        Vector2 NewDirection = -Direction;
        NewDirection.Normalize();
        return NewDirection;
    }
}
