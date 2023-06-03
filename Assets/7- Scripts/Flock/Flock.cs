using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Flock : MonoBehaviour
{
    [Header("Spawn")]
    public FlockAgent agentPrefab;
    [Range(0, 50)] public int startingCount;
    public float agentDensity = 0.08f;

    [Header("Ownership")]
                            public bool isPlayer;
                            public GameObject chef;

    [Header("Agents Behaviour")]
                            public List<FlockAgent> agents = new List<FlockAgent>();
                            public FA_Behaviour flockAgentBehaviour;

    [Header("Agent Type")]
                            public bool isAgressif;
                            public bool isPacifique;

    [Header("Agent Movement")]
    [Range(0.05f, 100f)]    public float driveFactor = 10f;
    [Range(0f, 100f)]       public float maxSpeed = 5f;
    [Range(0.05f, 10f)]     public float neighborRadius = 1.5f;
    [Range(0f, 20f)]        public float avoidanceRadiusMultiplier = 0.5f;
    [HideInInspector]       public float squareMaxSpeed;
                            float squareNeighborRadius;
                            float squareAvoidanceRadius;
                            public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    [Header("Charge")]
                            public float LauchForce;
                            public float ChargedTime;
                            public float RepulseForce;
                            public int DMG;
                            public float radius;
    [HideInInspector]       public int pourcentAgro = 71;
    [HideInInspector]       public FlockAgent Target;
    [HideInInspector]       public bool IsLaunch = false;
    [HideInInspector]       public Vector2 DistancePos;
    [HideInInspector]       public float Ennemidistance;
    [HideInInspector]       public bool ennemis;

    [Header("Conversion")]
                            public int MaxConvert;
    [SerializeField]        float Multiplicater;
    [HideInInspector]       public bool HasConvert = false;

    [Header("Lifetime")]
                            public bool destroy = true;
                            public float TIMER = 2f;
                            public float timebtwDeath;
                            public bool addnew = true;
                            public int compteur;
    [HideInInspector]       public float timer;
    [HideInInspector]       public float timer2;

    [HideInInspector]       public FlockSpawn flockSpawn;
    [HideInInspector]       public FlockAggro flockAggro;
    [HideInInspector]       public FlockLifetime flockLifetime;
    [HideInInspector]       public FlockMovement flockMovement;
    [HideInInspector]       public FlockFear flockFear;

    private void Awake()
    {
        flockSpawn = GetComponent<FlockSpawn>();
        flockAggro = GetComponent<FlockAggro>();
        flockLifetime = GetComponent<FlockLifetime>();
        flockMovement = GetComponent<FlockMovement>();
        flockFear = GetComponent<FlockFear>();
    }

    void Start()
    {
        LifetimeSetup();
        MovementSetup();

        flockSpawn.SpawnAgents();
    }

    void LifetimeSetup()
    {
        timer = TIMER;
    }

    void MovementSetup()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
    }

    void Update()
    {
        flockAggro.Aggro();
        flockLifetime.IncrementTimedDead();
        flockMovement.GroupMovement();
        flockLifetime.TimedDead();
    }

    public IEnumerator ConvertOther(GameObject Pacifist, FlockAgent agent)
    {
        if(HasConvert == false)
        {
            flockAggro.ChaseAnotherEntity(Target, agent);
        }

        if(Pacifist != null)
        {
            float convertPercent = Pacifist.GetComponent<FlockAgent>().ConvertPercent; // To avoid nullreference exceptions errors

            for (int i = 0; convertPercent < MaxConvert; i++)
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

    public IEnumerator LifeTimer(int LifeTime)
    {
        destroy = false;
        Destroy(agents.Last());
        agents.Remove(agents.Last());
        yield return new WaitForSeconds(LifeTime);
        destroy = true;
    }
}
