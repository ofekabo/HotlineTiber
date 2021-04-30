using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InnocentRunState : InnocentState
{
    
    Vector3 destination;
    public InnocentStateId GetId()
    {
        return InnocentStateId.Run;
    }

    public void Enter(AiInnocent innocent)
    {
        innocent.locomotion.anim.SetTrigger("Run1");
        innocent.navMeshAgent.destination = GenerateRandomDestination(innocent);
    }

    public void Update(AiInnocent innocent)
    {
        if (!innocent.navMeshAgent.hasPath)
        {
            innocent.navMeshAgent.destination = GenerateRandomDestination(innocent);
        }
    }

    public void Exit(AiInnocent innocent)
    {
        
    }

    Vector3 GenerateRandomDestination(AiInnocent innocent)
    {
        Vector3 randomDirection = Random.insideUnitSphere * 25;
        randomDirection += innocent.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection,out hit, 25, NavMesh.AllAreas);
        Vector3 destination = hit.position;
        return destination;
    }
}
