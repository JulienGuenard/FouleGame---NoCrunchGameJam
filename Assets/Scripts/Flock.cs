using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public GameObject chef;
    public float TIMER = 2f;

    float timer;

    public FlockAgent agentPrefab;
    public List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;
    public int compteur;

    public bool isAgressif;
    public bool isPacifique;

    public bool addnew = true;

    [Range(10, 500)]
    public int strartingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(0f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 20f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius;  } }

    // Start is called before the first frame update
    void Start()
    {
        timer = TIMER;
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < strartingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitSphere * strartingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0,360f)),
                transform
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!addnew)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                addnew = true;
                timer = TIMER;
            }
        }
        foreach(FlockAgent agent in agents.ToArray())
        {
            if (!agent.GetComponent<MouseInteraction>().isSelected)
            {
                float distance = Vector2.Distance(chef.transform.position, agent.transform.position);
                float Speed = Mathf.Clamp(distance, 1, maxSpeed);
                List<Transform> context = GetNearbyObjects(agent);
                Vector2 move = behavior.CalculateMove(agent, context, this, chef.transform.position);
                move *= driveFactor;
                if (move.sqrMagnitude > squareMaxSpeed)
                {
                    move = move.normalized * Speed;
                }
                agent.Move(move);
            }
            
        }
        compteur = agents.Count;
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach(Collider2D c in contextColliders)
        {
            if(c != agent.AgentCollider && !c.CompareTag("Cursor"))
            {
                context.Add(c.transform);

                if(c.transform.parent.CompareTag("Neutre")&&addnew)
                {
                    Flock cflock = c.GetComponentInParent<Flock>();

                    cflock.agents.Remove(c.GetComponent<FlockAgent>());
                    c.transform.SetParent(this.transform, true);
                    agents.Add(c.GetComponent<FlockAgent>());
                    addnew = false;
                }
            }

        }
        return context;
    }
}
