using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameManager.Player;
    }

    void Update()
    {
        transform.position = player.transform.position;
    }
}
