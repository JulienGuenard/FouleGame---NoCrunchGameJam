using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockCharge : Flock
{
    [Header("Charge")]
                        public float    launchForce;
                        public float    chargedTime;
                        public float    repulseForce;
                        public int      damage;
                        public float    radius;
                        public float    attackRange;
    [HideInInspector]   public bool     isLaunch = false;
    [HideInInspector]   public Vector2  distancePos;
    [HideInInspector]   public float    ennemidistance;
    [HideInInspector]   public bool     ennemis;

    public void Charge(FlockAgent agent)
    {
        StartCoroutine(ChargedAttack(agent));
    }

    public IEnumerator ChargedAttack(FlockAgent agent)
    {
        yield return new WaitForSeconds(chargedTime);

        if (agent != null) StartCoroutine(ChargeMove(agent));
    }

    public IEnumerator ChargeMove(FlockAgent agent)
    {
        if (FAggro.targetOnAggro == null)   ChargeEnd(agent);
        else                                MoveToTarget(agent);

        yield return new WaitForSeconds(chargedTime);

        if (agent != null && FAggro.targetOnAggro != null)
        {
            NewEnemyDistance(agent);
            CheckAttack(agent);
        }

        isLaunch = false;
        ennemis = false;
    }

    void ChargeEnd(FlockAgent agent) { agent.agentAnimation.EndChargeAnimation(); }

    void MoveToTarget(FlockAgent agent)
    {
        float LaucnhDirectionX = FAggro.targetOnAggro.transform.position.x;
        float LaucnhDIrectionY = FAggro.targetOnAggro.transform.position.y;
        float posX = agent.transform.position.x;
        float posY = agent.transform.position.y;

        Vector2 lauchDir = new Vector2(LaucnhDirectionX - posX, LaucnhDIrectionY - posY).normalized;
        distancePos = lauchDir;
        agent.agentMovement.Move(lauchDir * launchForce);
        isLaunch = true;

        agent.agentAnimation.ChargeAnimation();
    }

    void NewEnemyDistance(FlockAgent agent)
    {
        ennemidistance = Vector2.Distance(FAggro.targetOnAggro.transform.position, agent.transform.position);
    }

    void CheckAttack(FlockAgent agent)
    {
        if (ennemidistance > attackRange) return;

        FAggro.targetOnAggro.transform.GetComponent<FlockAgent>().agentLife.TakeDamage(damage);
        agent.agentMovement.Move(-distancePos * repulseForce);
    }
}
