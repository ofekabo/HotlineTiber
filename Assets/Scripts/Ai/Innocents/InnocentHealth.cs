using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnocentHealth : Health
{
    private AiInnocent _innocent;
    public override void OnStart()
    {
        _innocent = GetComponent<AiInnocent>();
    }

    public override void OnDeath(Vector3 direction,float force)
    {
        _innocent.stateMachine.ChangeState(InnocentStateId.Death);
        InnocentDeathState deathState = _innocent.stateMachine.GetState(InnocentStateId.Death) as InnocentDeathState;
        deathState.Direction = direction;
        deathState.ForceFromWep = force;
        _innocent.capsuleCollider.enabled = false;
    }

    public override void OnDamage(Vector3 direction, float force)
    {
        if (_innocent.stateMachine.currentState == InnocentStateId.Idle)
        {
            _innocent.locomotion.anim.SetTrigger("Run1");
            _innocent.stateMachine.ChangeState(InnocentStateId.Run);
        }
        
        
    }
}
