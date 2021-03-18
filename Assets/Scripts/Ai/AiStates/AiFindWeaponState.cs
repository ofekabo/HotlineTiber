﻿using System.Collections;
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
        WeaponPickup pickupOld = FindClosetWeapon(agent);
        agent.navMeshAgent.destination = pickupOld.transform.position;
        agent.navMeshAgent.speed = 5;
    }

    public void Update(AiAgent agent)
    {

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
