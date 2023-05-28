using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{

    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    public bool movementIsEnabled = true;

    // Start is called before the first frame update
    
    public void Initialize(Flock flock)
    {

    }

    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }
    public void Move(Vector2 velocity)
    {
        if (!movementIsEnabled) return;

        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
