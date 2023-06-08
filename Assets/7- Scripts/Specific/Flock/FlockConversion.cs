using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockConversion : Flock
{
    [Header("Conversion")]
    public int MaxConvert;
    [SerializeField] float Multiplicater;
    [HideInInspector] public bool HasConvert = false;

    [HideInInspector] public Flock Flk;

    public void PassifConversion()  // Appelé par UpdateEvent (voir inspector)
    {
        foreach (FlockAgent agent in FBehaviour.agents.ToArray())
        {
            if (this.tag != "Neutre")       continue;
            if (agent.agentConversion.ConvertPercent <= 0)  continue;

            agent.agentConversion.ConvertPercent = 0;
        }
    }

    public void Conversion() // Appelé par UpdateEvent (voir inspector)
    {
        foreach (FlockAgent agent in FBehaviour.agents.ToArray())
        {
            if (agent == null) continue;

            CheckHP(agent);

            if (agent == null) continue;

            CheckConversion(agent);
        }
    }

    void CheckHP(FlockAgent agent)
    {
        if (agent.agentSelection.isDragged) return;

        agent.agentLife.CheckHP();
    }

    void CheckConversion(FlockAgent agent)
    {
        if (agent.agentConversion.ConvertPercent < 100) return;

        if (agent.agentConversion.ConvertPercent >= 100) agent.agentConversion.ConvertPercent = 0;

        FBehaviour.agents.Remove(agent);
        agent.transform.SetParent(PlayerManager.instance.flockPaco.transform, true);
        PlayerManager.instance.flockPaco.FBehaviour.agents.Add(agent);
        agent.agentOwnership.parentflock = PlayerManager.instance.flockPaco;

        if (FBehaviour.agents.Count == 0) Destroy(gameObject);
    }

    public void Convert(FlockAgent agent, FlockAgent target)
    {
        FConversion.HasConvert = false;
        FConversion.MaxConvert = 100;

        if (FAggro.targetOnAggro.tag == "agressif") return;

        if (agent == null) return;

        if (FConversion.HasConvert == false && FAggro.targetOnAggro != null) 
                StartCoroutine(ConvertOther(FAggro.targetOnAggro.gameObject, agent));
        else    agent.agentMovement.Move(-FFear.Fear(agent.transform.position - target.transform.position) * 2);

    }

    public IEnumerator ConvertOther(GameObject Pacifist, FlockAgent agent)
    {
        if (HasConvert == false) FAggro.ChaseAnotherEntity(FAggro.targetOnAggro, agent);

        if (Pacifist == null) yield break;

        float convertPercent = Pacifist.GetComponent<FlockAgent>().agentConversion.ConvertPercent; // To avoid nullreference exceptions errors

        for (int i = 0; convertPercent < MaxConvert; i++)
        {
            if (Pacifist == null) break;

            Pacifist.GetComponent<FlockAgent>().agentConversion.ConvertPercent += Multiplicater;

            yield return new WaitForSeconds(0.5f);

            if (Pacifist == null) break;

            if (FAggro.targetOnAggro == null && Pacifist.GetComponent<FlockAgent>().agentConversion.ConvertPercent >= 0) 
            { HasConvert = true; break; }

            if (Pacifist.GetComponent<FlockAgent>().agentConversion.ConvertPercent == MaxConvert) 
            { HasConvert = true; break; }
        }
    }
}
