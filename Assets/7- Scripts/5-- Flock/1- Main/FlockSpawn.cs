using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockSpawn : Flock
{
    [Header("Spawn")]
    public FlockAgent agentPrefab;
    public GameObject chefPrefab;
    public bool noChef;
    [Range(0, 50)] public int startingCount;
    public float agentDensity = 0.08f;
    static int agentID = 0;

    public GameObject attackGMB;

    private void Start()
    {
        SpawnAgents();
    }

    public void SpawnAgents() 
    { 
        StartCoroutine(SpawnDelayer()); 
    }

    IEnumerator SpawnDelayer()
    {
        if (FOwnership.chef == null)
        {
            FOwnership.chef = Instantiate(chefPrefab, transform.position, Quaternion.identity);
        }

        for (int i = 0; i < startingCount; i++)
        {
            yield return new WaitForSeconds(0.01f);
            FlockAgent newAgent = Instantiate
            (
                agentPrefab,
                FOwnership.chef.transform.position + Random.insideUnitSphere * startingCount * agentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)),
                transform
            );

            AttackManager.instance.SpawnNewAttack();

            agentID++;
            newAgent.name = "#" + agentID + " Agent " + i;
            newAgent.agentOwnership.Initialize(GetComponent<Flock>());
            FBehaviour.agents.Add(newAgent);
        }

        if (noChef)
        {
            Destroy(FOwnership.chef);
            FOwnership.chef = null;
        }
        StartCoroutine(AgentsActivationDelayer());
    }

    IEnumerator AgentsActivationDelayer()
    {
        yield return new WaitForSeconds(0.01f);
        foreach (FlockAgent agent in FBehaviour.agents)
        {
            yield return new WaitForSeconds(0.01f);
            yield return new WaitForFixedUpdate();
            agent.agentCooldown.canCalculateMove = true;
            agent.agentCooldown.canCheckEnemies = true;
            agent.agentCooldown.UnableCheckEnemies();
        }
    }
}
