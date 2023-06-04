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

    public IEnumerator ConvertOther(GameObject Pacifist, FlockAgent agent)
    {
        if (HasConvert == false)
        {
            FAggro.ChaseAnotherEntity(FAggro.Target, agent);
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
                    if (FAggro.Target == null && Pacifist.GetComponent<FlockAgent>().ConvertPercent >= 0)
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
