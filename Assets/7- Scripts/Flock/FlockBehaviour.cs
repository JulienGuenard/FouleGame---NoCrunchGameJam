using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockBehaviour : Flock
{
    [Header("Agents Behaviour")]
    public List<FlockAgent> agents = new List<FlockAgent>();
    public FA_Behaviour flockAgentBehaviour;
}
