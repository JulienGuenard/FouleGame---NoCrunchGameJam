using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : MonoBehaviour
{
    [HideInInspector] public FlockAgent target;
    [HideInInspector] public int damage;
}
