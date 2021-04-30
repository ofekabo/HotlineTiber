using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDeathState : AiState
{
    public Vector3 Direction;
    public float ForceFromWep;

    public AiStateId GetId()
    {
        return AiStateId.Death;
        
    }

    public void Enter(AiAgent agent)
    {
        agent.ragdoll.ActivateRagdoll();
        Direction.y = 0.5f;
        agent.ragdoll.ApplyForce(Direction * ForceFromWep);
        agent.healthBar.gameObject.SetActive(false);
        agent.weapons.DropWeaon();
        agent.weapons.enabled = false;
        agent.navMeshAgent.enabled = false;
    }

    public void Update(AiAgent agent)
    {
        
    }

    public void Exit(AiAgent agent)
    {
        
    }
}
