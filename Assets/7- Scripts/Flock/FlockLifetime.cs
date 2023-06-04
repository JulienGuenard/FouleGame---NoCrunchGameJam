using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlockLifetime : Flock
{
    [Header("Lifetime")]
                        public bool destroy = true;
                        public float TIMER = 2f;
                        public float timebtwDeath;
                        public bool addnew = true;
    [HideInInspector]   public int compteur;
    [HideInInspector]   public float timer;
    [HideInInspector]   public float timer2;

    private void Start()
    {
        LifetimeSetup();
    }

    private void Update()
    {
        FLifetime.IncrementTimedDead();
        FLifetime.TimedDead();
    }

    void LifetimeSetup()
    {
        timer = TIMER;
    }

    public void IncrementTimedDead()
    {
        if (!addnew)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                addnew = true;
                timer = TIMER;
            }
        }
    }

    public void TimedDead()
    {
        compteur = FBehaviour.agents.Count;
        if (FOwnership.isPlayer && FBehaviour.agents.Count != 0)
        {
            timer2 -= Time.deltaTime;
            if (timer2 <= 0)
            {
                FBehaviour.agents.First().flockAgentAnimation.DeadAnimation();

                Destroy(FBehaviour.agents.First().gameObject);
                FBehaviour.agents.Remove(FBehaviour.agents.First());

                timer2 = timebtwDeath;
            }
        }
    }

    public IEnumerator LifeTimer(int LifeTime)
    {
        destroy = false;
        Destroy(FBehaviour.agents.Last());
        FBehaviour.agents.Remove(FBehaviour.agents.Last());
        yield return new WaitForSeconds(LifeTime);
        destroy = true;
    }
}
