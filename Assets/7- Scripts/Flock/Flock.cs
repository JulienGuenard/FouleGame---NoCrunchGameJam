using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [HideInInspector] public FlockSpawn             FSpawn;
    [HideInInspector] public FlockAggro             FAggro;
    [HideInInspector] public FlockLifetime          FLifetime;
    [HideInInspector] public FlockMovement          FMovement;
    [HideInInspector] public FlockFear              FFear;
    [HideInInspector] public FlockOwnership         FOwnership;
    [HideInInspector] public FlockType              FType;
    [HideInInspector] public FlockConversion        FConversion;
    [HideInInspector] public FlockBehaviour         FBehaviour;
    [HideInInspector] public FlockCharge            FCharge;
    [HideInInspector] public FlockGetAgentFunctions FGetAgentFunctions;
    [HideInInspector] public FlockDeath             FDeath;
    [HideInInspector] public FlockEvents            FEvents;

    public virtual void Awake()
    {
        FSpawn                  = GetComponent<FlockSpawn>();
        FAggro                  = GetComponent<FlockAggro>();
        FLifetime               = GetComponent<FlockLifetime>();
        FMovement               = GetComponent<FlockMovement>();
        FFear                   = GetComponent<FlockFear>();
        FOwnership              = GetComponent<FlockOwnership>();
        FType                   = GetComponent<FlockType>();
        FConversion             = GetComponent<FlockConversion>();
        FBehaviour              = GetComponent<FlockBehaviour>();
        FCharge                 = GetComponent<FlockCharge>();
        FGetAgentFunctions      = GetComponent<FlockGetAgentFunctions>();
        FDeath                  = GetComponent<FlockDeath>();
        FEvents                 = GetComponent<FlockEvents>();
    }
}
