using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlockLifetime : Flock
{
    [Header("Lifetime")]
                        public bool destroy = true;
                        public float deathByTimeDelay;
    [HideInInspector]   public float deathByTimeActual;

    private void Start()
    {
        deathByTimeActual = deathByTimeDelay;
    }

    private void Update()
    {
        FLifetime.IncrementNextTimedDead();
    }

    public void IncrementNextTimedDead()
    {
        if (FOwnership.isPlayer)            return;
        if (FBehaviour.agents.Count == 0)   return;

        deathByTimeActual -= Time.deltaTime;

        if (deathByTimeActual > 0)          return;

        Death(FBehaviour.agents.First());
        deathByTimeActual = deathByTimeDelay;
    }

    void Death(FlockAgent agent)
    {
        FBehaviour.agents.First().flockAgentAnimation.DeadAnimation();

        Destroy(FBehaviour.agents.First().gameObject);
        FBehaviour.agents.Remove(FBehaviour.agents.First());
    }
}
