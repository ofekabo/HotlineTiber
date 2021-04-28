using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : Health
{
    private AiAgent agent;

    public override void OnStart()
    {
        agent = GetComponent<AiAgent>();
    }
    public override void OnDeath(Vector3 direction,float force)
    {
        agent.stateMachine.ChangeState(AiStateId.Death);
        AiDeathState deathState = agent.stateMachine.GetState(AiStateId.Death) as AiDeathState;
        deathState.Direction = direction;
        deathState.ForceFromWep = force;
        agent.capsuleCollider.enabled = false;
        


    }
    public override void OnDamage(Vector3 direction,float force)
    {
        agent.stateMachine.ChangeState(AiStateId.AttackPlayer);
    }
}
