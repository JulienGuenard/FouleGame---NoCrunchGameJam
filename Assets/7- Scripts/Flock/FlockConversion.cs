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

    private void Update()
    {
        PassifConversion();
        Conversion();
    }

    void PassifConversion()
    {
        foreach (FlockAgent agent in FBehaviour.agents.ToArray())
        {
            if (this.tag != "Neutre")       continue;
            if (agent.ConvertPercent <= 0)  continue;

            agent.ConvertPercent = 0;
        }
    }

    void Conversion()
    {
        foreach (FlockAgent agent in FBehaviour.agents.ToArray())
        {
            if (agent == null && FOwnership.chef == null)
            {
                if (agent.mouseInteraction.isSelected)
                {
                    agent.CheckHP();
                }
            }

                    if ((FOwnership.chef == null && agent != null) || (agent.ConvertPercent == 100 && agent != null))
            {
                if (agent.ConvertPercent > 0)
                {
                    agent.ConvertPercent = 0;
                }
                FBehaviour.agents.Remove(agent);
                agent.transform.SetParent(GameManager.neutre.transform, true);
                GameManager.neutreFlock.FBehaviour.agents.Add(agent);
                if (FBehaviour.agents.Count == 0)
                {
                    Destroy(gameObject);
                }
            }

            if (agent == null)
            {
                FBehaviour.agents.Remove(agent);
            }
        }
    }

    public void Convert(FlockAgent agent, FlockAgent target)
    {
        FConversion.HasConvert = false;
        FConversion.MaxConvert = 100;

        if (FAggro.targetOnAggro.tag == "agressif") return;

        if (FConversion.HasConvert == false && FAggro.targetOnAggro != null) StartCoroutine(FConversion.ConvertOther(FAggro.targetOnAggro.gameObject, agent));
        else agent.Move(-FFear.Fear(agent.transform.position - target.transform.position) * 2);
    }

    public IEnumerator ConvertOther(GameObject Pacifist, FlockAgent agent)
    {
        if (HasConvert == false)
        {
            FAggro.ChaseAnotherEntity(FAggro.targetOnAggro, agent);
        }

        if (Pacifist != null)
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
                if (Pacifist != null)
                {
                    if (FAggro.targetOnAggro == null && Pacifist.GetComponent<FlockAgent>().ConvertPercent >= 0)
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
}
