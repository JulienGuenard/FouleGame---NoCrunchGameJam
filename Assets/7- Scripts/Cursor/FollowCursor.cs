using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public Vector3 direction;
    private Vector3 lastPos;

    public static FollowCursor instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        lastPos = transform.position;
        transform.position = GameManager.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        float distance = (lastPos - transform.position).magnitude;
        distance *= 1000;

        if (distance > 300f || distance < -300f)
        {
            direction.z = Vector2.Angle(lastPos, transform.position);
            transform.rotation = Quaternion.Euler(direction * 100);
        }
    }
}
