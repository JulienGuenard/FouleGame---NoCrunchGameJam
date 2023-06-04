using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockSpawn : Flock
{
    [Header("Spawn")]
    public FlockAgent agentPrefab;
    [Range(0, 50)] public int startingCount;
    public float agentDensity = 0.08f;

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
        for (int i = 0; i < startingCount; i++)
        {
            yield return new WaitForSeconds(0.01f);
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                FOwnership.chef.transform.position + Random.insideUnitSphere * startingCount * agentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)),
                transform
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(GetComponent<Flock>());
            FBehaviour.agents.Add(newAgent);
        }

        StartCoroutine(AgentsActivationDelayer());
    }

    IEnumerator AgentsActivationDelayer()
    {
        yield return new WaitForSeconds(1f);

        foreach (FlockAgent agent in FBehaviour.agents)
        {
            yield return new WaitForSeconds(0.01f);
            agent.canCalculateMove = true;
            agent.canCheckEnemies = true;
        }
    }
}
