using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AiAttackPlayerState : AiState
{
    

    public AiStateId GetId()
    {
        return AiStateId.AttackPlayer;
    }

    public void Enter(AiAgent agent)
    {
        agent.weapons.SetWeightAiming(1);
        agent.weapons.weaponActive = true;
        agent.weapons.DrawWeapon(true);
    }

    public void Update(AiAgent agent)
    {

        agent.transform.LookAt(agent.playerTransform.position, Vector3.up);
        if (Time.time > agent.weapons.currentWeapon.nextRandom)
        {
            agent.weapons.SetTarget(agent.playerTransform);
            agent.weapons.currentWeapon.nextRandom = Time.time + agent.weapons.currentWeapon.randomSpeed;
        }

        if ((agent.transform.position - agent.playerTransform.position).sqrMagnitude 
            >
            agent.weapons.currentWeapon.shootingRange * agent.weapons.currentWeapon.shootingRange)
        {
            agent.navMeshAgent.destination = agent.playerTransform.position;
        }
       
    }

    public void Exit(AiAgent agent)
    {
        // agent.navMeshAgent.stoppingDistance = 0.0f;
    }
}
