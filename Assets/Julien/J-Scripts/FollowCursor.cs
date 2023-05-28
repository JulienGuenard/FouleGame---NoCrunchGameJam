using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    void Update()
    {
        transform.position = GameManager.MainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
    }
}
