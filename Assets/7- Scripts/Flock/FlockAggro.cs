using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAggro : Flock
{
    [HideInInspector] public int pourcentAgro = 71;
    [HideInInspector] public FlockAgent Target;

    private void Update()
    {
        Aggro();
    }

    public void Aggro()
    {
        if (FOwnership.isPlayer && PlayerManager.instance.compteurTotal != 0)
        {
            pourcentAgro = (100 * PlayerManager.instance.compteurAggro) / PlayerManager.instance.compteurTotal;
        }
    }

    public void ChaseAnotherEntity(FlockAgent other, FlockAgent agent)
    {
        float Distance = Vector2.Distance(agent.transform.position, other.transform.position);
        Vector2 direction = other.transform.position - agent.transform.position;
        direction.Normalize();

        /// Pour que ça tourn de manière smoooooooooooooth
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        agent.transform.position = Vector2.MoveTowards(agent.transform.position, other.transform.position, 1 * Time.deltaTime);
        agent.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        /*        if (Distance <= 0.2)
                {

                }*/

    }
}
