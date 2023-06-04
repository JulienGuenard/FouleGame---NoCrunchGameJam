using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockCharge : Flock
{
    [Header("Charge")]
    public float LauchForce;
    public float ChargedTime;
    public float RepulseForce;
    public int DMG;
    public float radius;
    [HideInInspector] public bool IsLaunch = false;
    [HideInInspector] public Vector2 DistancePos;
    [HideInInspector] public float Ennemidistance;
    [HideInInspector] public bool ennemis;

    public IEnumerator ChargedAttack(FlockAgent agent)
    {
        yield return new WaitForSeconds(ChargedTime);

        if (agent != null)
        {
            StartCoroutine(Charge(agent));
        }
    }

    public IEnumerator Charge(FlockAgent agent)
    {
        if (Target != null)
        {
            float LaucnhDirectionX = Target.transform.position.x;
            float LaucnhDIrectionY = Target.transform.position.y;
            float posX = agent.transform.position.x;
            float posY = agent.transform.position.y;

            Vector2 lauchDir = new Vector2(LaucnhDirectionX - posX, LaucnhDIrectionY - posY).normalized;
            DistancePos = lauchDir;
            agent.Move(lauchDir * LauchForce);
            IsLaunch = true;

            agent.flockAgentAnimation.ChargeAnimation();
        }
        else
        {
            agent.flockAgentAnimation.EndChargeAnimation();
        }


        yield return new WaitForSeconds(ChargedTime);
        if (agent != null && FAggro.Target != null)
        {
            Ennemidistance = Vector2.Distance(FAggro.Target.transform.position, agent.transform.position);
        }


        if (Ennemidistance <= 0.2 && FAggro.Target != null && agent != null)
        {
            CheckAttack(agent);
        }
        IsLaunch = false;


        ennemis = false;
    }

    public void CheckAttack(FlockAgent agent)
    {
        FAggro.Target.transform.GetComponent<FlockAgent>().TakeDamage(DMG);
        agent.Move(-DistancePos * RepulseForce);
    }
}
