using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFindWeaponState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.FindWeapon;
    }

    public void Enter(AiAgent agent)
    {
        if (agent.weapons.hasWeapon)
        {
            agent.stateMachine.ChangeState(AiStateId.Idle);
        }
        
        if (!agent.weapons.hasWeapon)
        {
            WeaponPickup pickup = FindClosetWeapon(agent);
            agent.navMeshAgent.destination = pickup.transform.position;
            agent.navMeshAgent.speed = 5;
        }

    }

    public void Update(AiAgent agent)
    {
        if (agent.weapons.HasWeapon() && Vector3.Distance(agent.transform.position,agent.playerTransform.position) < agent.config.shootingRange)
        {
            agent.stateMachine.ChangeState(AiStateId.AttackPlayer);
        }
    }

    public void Exit(AiAgent agent)
    {

    }

    private WeaponPickup FindClosetWeapon(AiAgent agent)
    {
        WeaponPickup[] weapons = Object.FindObjectsOfType<WeaponPickup>();
        WeaponPickup closestWeapon = null;
        float closestDistance = float.MaxValue;
        foreach (var weapon in weapons)
        {
            float distanceToWeapon = Vector3.Distance(agent.transform.position, weapon.transform.position);
            if (distanceToWeapon < closestDistance)
            {
                closestDistance = distanceToWeapon;
                closestWeapon = weapon;
            }
        }

        return closestWeapon;
    }
}
