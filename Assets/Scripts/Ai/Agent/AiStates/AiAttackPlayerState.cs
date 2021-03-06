﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AiAttackPlayerState : AiState
{
    
    private LayerMask layerProp;
    private float _delayInterval;

    public AiStateId GetId()
    {
        return AiStateId.AttackPlayer;
    }

    public void Enter(AiAgent agent)
    {
        agent.weapons.SetWeightAiming(1);
        agent.weapons.DrawWeapon(true);
        layerProp = LayerMask.NameToLayer("Prop");
    }

    public void Update(AiAgent agent)
    {
        
        float sqrDistancePfromA = (agent.playerTransform.position - agent.weapons.currentWeapon.shootingPoint.position).sqrMagnitude;
        float sqrShootingRange = agent.weapons.currentWeapon.shootingRange * agent.weapons.currentWeapon.shootingRange;
        
       //Debug.Log("distance : " +sqrDistancePfromA + "\n range :" + sqrShootingRange);
        
        PlayerInSightCheck(sqrDistancePfromA,sqrShootingRange,agent);
        
        agent.transform.LookAt(agent.playerTransform.position, Vector3.up);
        
        if (Time.time > agent.weapons.currentWeapon.nextRandom)
        {
            agent.weapons.SetTarget(agent.playerTransform);
            agent.weapons.currentWeapon.nextRandom = Time.time + agent.weapons.currentWeapon.randomSpeed;
        }

        if (sqrDistancePfromA > sqrShootingRange)
        {
            agent.navMeshAgent.destination = agent.playerTransform.position;
        }

        if (!agent.weapons.weaponActive && sqrDistancePfromA < sqrShootingRange) { return; }
     
        // implement going around sphere if shooting
        
        
    }

    public void Exit(AiAgent agent)
    {
        // agent.navMeshAgent.stoppingDistance = 0.0f;
    }

    
    
    void PlayerInSightCheck(float sqrDistancePfromA, float sqrShootingRange,AiAgent agent)
    {
        bool playerInSight = RaycastCheckIsPlayerInSight(agent, sqrDistancePfromA,sqrShootingRange);
        if (playerInSight)
        {
            _delayInterval += Time.deltaTime;
            if(!agent.weapons.weaponActive && _delayInterval > agent.weapons.DelayTillAttacking)
                agent.weapons.weaponActive = true;
        }
        if(!playerInSight)
        {
            agent.weapons.weaponActive = false;
            _delayInterval = 0;
        }
    }
    
    bool RaycastCheckIsPlayerInSight(AiAgent agent , float sqrDistancePfromA, float sqrShootingRange)
    {
        if(agent.weapons.currentWeapon.weaponType == RaycastWeapon.WeaponType.MeleeWeapon) { return true; }
        Ray ray = new Ray(agent.weapons.currentWeapon.shootingPoint.position,agent.weapons.currentWeapon.shootingPoint.forward);
        
        if(agent.weapons.showWeaponRange)
            Debug.DrawRay(ray.origin,ray.direction * agent.weapons.currentWeapon.shootingRange, Color.magenta);
        
        if (Physics.Raycast(ray, out RaycastHit hit, agent.weapons.currentWeapon.shootingRange))
        {
            if(hit.collider.GetComponent<PlayerController>() || hit.transform.gameObject.layer == layerProp) { return true; }
            if(!hit.collider.GetComponent<PlayerController>()) { return false; }
        }
        
        if(sqrDistancePfromA < sqrShootingRange) { return true; }
        if(sqrDistancePfromA > sqrShootingRange) { return false; }
        
        return false;
    }
}
