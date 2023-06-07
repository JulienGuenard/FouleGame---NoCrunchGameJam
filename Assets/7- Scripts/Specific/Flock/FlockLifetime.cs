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

    public void IncrementNextTimedDead() // Appelé par FlockEvent (voir inspector)
    {
        if (FOwnership.isPlayer)            return;
        if (FBehaviour.agents.Count == 0)   return;

        deathByTimeActual -= Time.deltaTime;

        if (deathByTimeActual > 0)          return;

        FDeath.Death(FBehaviour.agents.First());
        deathByTimeActual = deathByTimeDelay;
    }
}
