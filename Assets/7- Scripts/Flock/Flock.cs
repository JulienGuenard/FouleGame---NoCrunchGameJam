using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
   
    [SerializeField] int MaxConvert;
    [SerializeField] float Multiplicater;

    public bool destroy = true;

    bool HasConvert = false;

    private Vector2 DistancePos;


    public GameObject chef;
    public float TIMER = 2f;

    float timer;
    float timer2;

    public float timebtwDeath;

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

    int pourcentAgro = 71;

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
        if (isPlayer && GameManager.Player.GetComponent<PlayerManager>().compteurTotal!=0)
        {
            pourcentAgro = (100 * GameManager.Player.GetComponent<PlayerManager>().compteurAggro) / GameManager.Player.GetComponent<PlayerManager>().compteurTotal;

         
        }
      
        
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
            if(this.tag == "Neutre")
            {
                if (agent.ConvertPercent > 0)
                {
                    agent.ConvertPercent = 0;
                }
            }
            if(agent != null&&chef != null)
            {
                if (!agent.GetComponent<MouseInteraction>().isSelected)
                {

                    agent.CheckHP();
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
            if((chef == null&&agent != null)||(agent.ConvertPercent == 100&&agent !=null))
            {
                if (agent.ConvertPercent > 0)
                {
                    agent.ConvertPercent = 0;
                }
                agents.Remove(agent);
                agent.transform.SetParent(GameManager.neutre.transform, true);
                GameManager.neutre.GetComponent<Flock>().agents.Add(agent);
                if(agents.Count == 0)
                {
                    Destroy(gameObject);
                }
        
            }
            if(agent == null)
            {
                agents.Remove(agent);
            }
     
            
        }
        compteur = agents.Count;
        if (isPlayer && agents.Count != 0)
        {
            timer2 -= Time.deltaTime;
            if (timer2 <= 0)
            {
                agents.First().flockAnimation.DeadAnimation();

                Destroy(agents.First().gameObject);
                agents.Remove(agents.First());
              
                timer2 = timebtwDeath;
            }
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach(Collider2D c in contextColliders)
        {
            if(c != agent.AgentCollider && !c.CompareTag("Cursor")&&c.transform.parent!=null)
            {
               
         
                if (c.transform.parent.CompareTag("Neutre")&&addnew&&isPlayer)
                {
                    Flock flockpassif = chef.GetComponent<PlayerManager>().flockPaco;
                    Flock flockaggressif = chef.GetComponent<PlayerManager>().flockAggro;
                    Flock cflock = c.GetComponentInParent<Flock>();

                    cflock.agents.Remove(c.GetComponent<FlockAgent>());
                    if(c.transform.tag == "passif")
                    {
                        c.transform.SetParent(flockpassif.transform, true);
                        flockpassif.agents.Add(c.GetComponent<FlockAgent>());
                        addnew = false;
                    }
                    if (c.transform.tag == "agressif")
                    {
                        c.transform.SetParent(flockaggressif.transform, true);
                        flockaggressif.agents.Add(c.GetComponent<FlockAgent>());
                        addnew = false;
                    }
                   
                }
                context.Add(c.transform);
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
               
                    if (isPlayer)
                    {
                        if (pourcentAgro <= 70)
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
                            if (i.transform.parent.tag != "Neutre"
                            && i.transform.parent.tag != "Untagged" && i.transform.parent.tag != "Cursor")
                            {
                                ennemis.Add(i.transform);
                                target = i.transform;
                                return true;
                            }
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
            if(i.transform.tag == "Chefs"&&isPlayer)
            {
                ennemis.Add(i.transform);
                target = i.transform;

                return true;
            }
          
        }
        target = null;
        return false;
       
    }

    bool GetPassif(FlockAgent agent, out Transform target)
    {
        List<Transform> ennemis = new List<Transform>();
        Collider2D[] ennemisCollider = Physics2D.OverlapCircleAll(agent.transform.position, radius);
        foreach (Collider2D i in ennemisCollider)
        {
            if (i.transform.gameObject.GetComponent<FlockAgent>() != null && i.transform.parent != null&&i.transform.tag == "passif")
            {
                if (isPlayer)
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
            if(i.transform.tag == "agressif" && i.transform.parent.tag != "PlayerFlock")
            {
                if (!agents.Contains(i.transform.GetComponent<FlockAgent>()) && i.transform.parent.tag != "Neutre" && i.transform.parent.tag != "PlayerFlock"
                && i.transform.parent.tag != "Untagged" && i.transform.parent.tag != "Cursor")
                {
                    ennemis.Add(i.transform);
                    target = i.transform;
                    return true;
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
        ennemis = GetPassif(agent, out Transform target);


        Vector2 move;

       

        if (target != null)
        {
            if (target.parent.tag == "PlayerFlock")
            {
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }
            Target = target;
        }

        if (ennemis == true && Target != null)
        {

            HasConvert = false;
            MaxConvert = 100;
            if (Target.tag != "agressif")
            {
                if (HasConvert == false && Target != null)
                {
                    StartCoroutine(ConvertOther(Target.gameObject, agent));
                }
            }
            else
            {
                agent.Move(-Fear(agent.transform.position - target.transform.position) * 2);
            }
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
    public IEnumerator ConvertOther(GameObject Pacifist, FlockAgent agent)
    {
        if(HasConvert == false)
        {
            ChaseAnotherEntity(Target, agent);
        }
   
       
        if(Pacifist != null)
        {
            for (int i = 0; Pacifist.GetComponent<FlockAgent>().ConvertPercent < MaxConvert; i++)
            {
                if (Pacifist == null)
                {
                    break;
                }
                if (Pacifist != null)
                {
                    Pacifist.GetComponent<FlockAgent>().ConvertPercent += Multiplicater;

                    yield return new WaitForSeconds(0.5f);


                }
                if(Pacifist != null)
                {
                    if (Target == null && Pacifist.GetComponent<FlockAgent>().ConvertPercent >= 0)
                    {
                        HasConvert = true;
                        break;
                    }
                }
   

                if (Pacifist != null)
                {
                    if (Pacifist.GetComponent<FlockAgent>().ConvertPercent == MaxConvert)
                    {
                        HasConvert = true;


                        break;
                    }
                }

            }
        }

    }
    public void ChaseAnotherEntity(Transform other, FlockAgent agent)
    {
        Debug.Log("adzdqdz");
            float Distance = Vector2.Distance(agent.transform.position, other.transform.position);
            Vector2 direction = other.transform.position - agent.transform.position;
            direction.Normalize();

            /// Pour que ça tourn de manière smoooooooooooooth
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            agent.transform.position = Vector2.MoveTowards(agent.transform.position, other.transform.position, 1*Time.deltaTime);
            agent.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
/*        if (Distance <= 0.2)
        {
            
        }*/
        
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

            agent.flockAnimation.ChargeAnimation();
        }else
        {
            agent.flockAnimation.EndChargeAnimation();
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
    public IEnumerator LifeTimer(int LifeTime)
    {
        destroy = false;
        Destroy(agents.Last());
        agents.Remove(agents.Last());
        yield return new WaitForSeconds(LifeTime);
        destroy = true;
        Debug.Log("Destroyed");


    }
    public Vector2 Fear(Vector2 Direction)
    {
        Vector2 NewDirection = -Direction;
        NewDirection.Normalize();
        return NewDirection;
    }

}
