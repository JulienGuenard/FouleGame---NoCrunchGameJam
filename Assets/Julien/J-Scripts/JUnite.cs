using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JUnite : MonoBehaviour
{
    public JUniteType uniteType;
    public Transform chefGroup;

    NavMeshAgent agent;
    

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis= false;
    }

    // Update is called once per frame
    void Update()
    {
        if (uniteType != JUniteType.Chef)
        {
            MoveToChef();
        }
    }

    void MoveToChef()
    {
       // agent.SetDestination(chefGroup.position);
    }
}
