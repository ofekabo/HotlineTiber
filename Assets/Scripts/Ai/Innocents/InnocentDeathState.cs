using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnocentDeathState : InnocentState
{
    
    public Vector3 Direction;
    public float ForceFromWep;
    
    public InnocentStateId GetId()
    {
        return InnocentStateId.Death;
    }

    public void Enter(AiInnocent innocent)
    {
        innocent.ragdollController.ActivateRagdoll();
        Direction.y = 0.5f;
        innocent.ragdollController.ApplyForce(Direction * ForceFromWep);
        // innocent.health.gameObject.SetActive(false);
        innocent.navMeshAgent.enabled = false;
    }

    public void Update(AiInnocent innocent)
    {
        
    }

    public void Exit(AiInnocent innocent)
    {
        
    }
}
