using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAggro : Flock
{
    [HideInInspector] public int pourcentAggro = 71;
    public FlockAgent targetOnAggro;

    private void Update()
    {
        Aggro();
    }

    public void Aggro()
    {
        if (!FOwnership.isPlayer)                       return;
        if (PlayerManager.instance.compteurTotal == 0)  return;

        pourcentAggro = (100 * PlayerManager.instance.compteurAggro) / PlayerManager.instance.compteurTotal;
    }

    public void ChaseAnotherEntity(FlockAgent other, FlockAgent agent)
    {
        float Distance = Vector2.Distance(agent.transform.position, other.transform.position);
        Vector2 direction = other.transform.position - agent.transform.position;
        direction.Normalize();

        /// Pour que �a tourn de mani�re smoooooooooooooth
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        agent.transform.position = Vector2.MoveTowards(agent.transform.position, other.transform.position, 1 * Time.deltaTime);
        agent.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
