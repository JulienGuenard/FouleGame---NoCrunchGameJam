using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public bool isPlayer;
    private Transform Target;
    [SerializeField] float LauchForce;
    [SerializeField] float ChargedTime;
    [SerializeField] float RepulseForce;
    private bool IsLaunch = false;
    [SerializeField] int DMG;
    [SerializeField] float radius;

    private Vector2 DistancePos;


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

    [Range(0, 500)]
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
    float Ennemidistance;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius;  } }

    bool ennemis;


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
                chef.transform.position + Random.insideUnitSphere * strartingCount * AgentDensity,
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
            if(agent != null)
            {
                if (!agent.GetComponent<MouseInteraction>().isSelected)
                {

                    agent.CheckHP();
                    if (agent.Health > 100)
                    {
                        Debug.Log(agent.Health);
                    }
                    if (isAgressif)
                    {
                        Agressif(agent);

                    }
                    else if (isPacifique)
                    {
                        Pacifique(agent);
                    }
                    else if (!isPacifique && !isAgressif)
                    {
                        Passif(agent);
                    }

                }
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
            if(c != agent.AgentCollider && !c.CompareTag("Cursor")&&c.transform.parent!=null)
            {
                context.Add(c.transform);
         
                if (c.transform.parent.CompareTag("Neutre")&&addnew)
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

    bool GetEnnemis(FlockAgent agent, out Transform target)
    {
        List<Transform> ennemis = new List<Transform>();
        Collider2D[] ennemisCollider = Physics2D.OverlapCircleAll(agent.transform.position, radius);
        foreach (Collider2D i in ennemisCollider)
        {
            if(i.transform.gameObject.GetComponent<FlockAgent>() != null&&i.transform.parent != null)
            {
                if(isPlayer)
                {
                   
                    
                    if (!agents.Contains(i.transform.GetComponent<FlockAgent>()) && i.transform.parent.tag != "Neutre" && i.transform.parent.tag != "PlayerFlock"
                     && i.transform.parent.tag != "Untagged" && i.transform.parent.tag != "Cursor")
                    {
                        ennemis.Add(i.transform);
                        target = i.transform;
                        return true;
                    }
                }
                else
                {
                  
                    if (!agents.Contains(i.transform.GetComponent<FlockAgent>()) && i.transform.parent.tag != "Neutre" && i.transform.parent.tag != "Ennemi"
                        && i.transform.parent.tag != "Untagged" && i.transform.parent.tag != "Cursor")
                    {
                        
                        ennemis.Add(i.transform);
                        target = i.transform;
                       
                        return true;
                    }
                }
           
            }
          
        }
        target = null;
        return false;
       
    }

    void Agressif(FlockAgent agent)
    {
        float distance = Vector2.Distance(chef.transform.position, agent.transform.position);
        float Speed = Mathf.Clamp(distance, 1, maxSpeed);
        List<Transform> context = GetNearbyObjects(agent);
        ennemis = GetEnnemis(agent,out Transform target);

        
        Vector2 move;
        
        if(target != null)
        {
            Target = target;
        }
        

        if (ennemis == true && Target != null)
        {
               
           
            
       
                StartCoroutine(ChargedAttack(agent)); 
        }
        else
        {
         
            move = behavior.CalculateMove(agent, context, this, chef.transform.position);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * Speed;
            }
            agent.Move(move);
        }   

     
    }
    public void CheckAttack(FlockAgent agent)
    {

            Target.transform.GetComponent<FlockAgent>().TakeDamage(DMG);



            agent.Move( - DistancePos * RepulseForce);
   
    }
    void Pacifique(FlockAgent agent)
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
    void Passif(FlockAgent agent)
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
    public IEnumerator ChargedAttack(FlockAgent agent)
    {

        yield return new WaitForSeconds(ChargedTime);
      

    
         if(agent != null)
        {
            StartCoroutine(Charge(agent));
        }
            
    
    }
    public IEnumerator Charge(FlockAgent agent)
    {

        if(Target != null)
        {
            float LaucnhDirectionX = Target.position.x;
            float LaucnhDIrectionY = Target.position.y;
            float posX = agent.transform.position.x;
            float posY = agent.transform.position.y;

            Vector2 lauchDir = new Vector2(LaucnhDirectionX - posX, LaucnhDIrectionY - posY).normalized;
            DistancePos = lauchDir;
            agent.Move(lauchDir * LauchForce);
            IsLaunch = true;
        }
      

            yield return new WaitForSeconds(ChargedTime);
        if(agent != null&&Target!=null)
        {
            Ennemidistance = Vector2.Distance(Target.position, agent.transform.position);
        }
        
  
        if (Ennemidistance <=0.2&& Target != null&&agent!=null)
            {
                CheckAttack(agent);
            }
            IsLaunch = false;

  
            ennemis = false;
           
    }


}
