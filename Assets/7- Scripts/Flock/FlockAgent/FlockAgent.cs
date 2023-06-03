using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : Flock
{
    Flock agentFlock;
    [HideInInspector] public FA_Animation flockAgentAnimation;
    [HideInInspector] public MouseInteraction mouseInteraction;
    [HideInInspector] public FA_Charge flockAgentCharge;

    [SerializeField] public float ConvertPercent = 0;

    public Rigidbody2D rb;

    public int Health = 100;
    public Flock AgentFlock { get { return agentFlock; } }

    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    public Flock parentflock;

    public bool canCheckEnemies = false;
    public bool canCalculateMove = false;
    public Vector2 move = Vector2.zero;

    // Start is called before the first frame update

    private void Awake()
    {
        flockAgentAnimation = GetComponent<FA_Animation>();
        mouseInteraction = GetComponent<MouseInteraction>();
        flockAgentCharge = GetComponent<FA_Charge>();
        canCheckEnemies = false;
        canCalculateMove = false;
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

    public void UnableCalculateMove()
    {
        if (!canCalculateMove) return;

        canCalculateMove = false;
        StartCoroutine(CalculateMoveDelay());
    }

    public void UnableCheckEnemies()
    {
        if (!canCheckEnemies) return;

        canCheckEnemies = false;
        StartCoroutine(CheckEnemiesDelay());
    }

    IEnumerator CalculateMoveDelay()
    {
        yield return new WaitForSeconds(8f * Time.deltaTime);
        canCalculateMove = true;
    }

    IEnumerator CheckEnemiesDelay()
    {
        yield return new WaitForSeconds(40f * Time.deltaTime);
        canCheckEnemies = true;
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
            flockAgentAnimation.DeadAnimation();
            parentflock.agents.Remove(this);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int Dmg)
    {
        this.Health -= Dmg;
       
        /*flockAnimation.DamagedAnimation();*/
    }

    public IEnumerator LifeTimer( int LifeTime)
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
}
