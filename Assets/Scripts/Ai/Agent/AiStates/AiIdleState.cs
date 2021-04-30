using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }

    public void Enter(AiAgent agent)
    {
       
    }

    public void Update(AiAgent agent)
    {
        
        // Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        // if (playerDirection.magnitude > agent.config.viewDistance)
        // {
        //     return;
        // }
        //
        // Vector3 agentDirection = agent.transform.forward;
        //
        // playerDirection.Normalize();
        //
        // float dotProduct = Vector3.Dot(playerDirection, agentDirection);
        //
        // if (dotProduct > 0.0)
        // {
        //     agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        // }
        
        if (agent.weapons.hasWeapon && Vector3.Distance(agent.transform.position,agent.playerTransform.position) < agent.config.viewDistance)
        {
            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }

    public void Exit(AiAgent agent) 
    {
        
    }
}
