using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    Flock agentFlock;
    [HideInInspector] public FlockAnimation flockAnimation;

    [SerializeField] public float ConvertPercent = 0;

    public Rigidbody2D rb;

    public int Health = 100;
    public Flock AgentFlock { get { return agentFlock; } }

    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    Flock parentflock;

    // Start is called before the first frame update

    private void Awake()
    {
        flockAnimation = GetComponent<FlockAnimation>();
    }

    public void Initialize(Flock flock)
    {

    }

    private void FixedUpdate()
    {
      
    }
    void Start()
    {
        parentflock = this.GetComponentInParent<Flock>();
        agentCollider = GetComponent<Collider2D>();
    }
    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
    public void RBMove(Vector2 dir, float force)
    {
        rb.velocity = dir * force;
    }
    public void CheckHP()
    {
        if (Health <= 0)
        {
            flockAnimation.DeadAnimation();
            parentflock.agents.Remove(this);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int Dmg)
    {
        this.Health -= Dmg;
       
        /*flockAnimation.DamagedAnimation();*/
    }

    public IEnumerator  LifeTimer( int LifeTime)
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
}
