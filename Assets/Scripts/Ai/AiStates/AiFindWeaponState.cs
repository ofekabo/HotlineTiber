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

    }

    public void Update(AiAgent agent)
    {

    }

    public void Exit(AiAgent agent)
    {

    }

    // private WeaponPickup FindclosetWeapon(AiAgent agent)
    // {
    //     WeaponPickup weapons = Object.FindObjectOfType<WeaponPickup>();
    //     WeaponPickup closestWeapon = null;
    //     float closestDistance = float.MaxValue;
    //     foreach (var weapon in weapons)
    //     {
    //         float distanceToWeapon = Vector3.Distance(agent.transform.position, weapon.transform.position);
    //         if (distanceToWeapon < closestDistance)
    //         {
    //             closestDistance = distanceToWeapon;
    //         }
    //     }
    // }
}
