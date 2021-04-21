using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AiAttackPlayerState : AiState
{
    
    float timer;
   
    
    public AiStateId GetId()
    {
        return AiStateId.AttackPlayer;
    }

    public void Enter(AiAgent agent)
    {
        agent.weapons.SetWeightAiming(1);
        agent.weapons.DrawWeapon(true);
    }

    public void Update(AiAgent agent)
    {
        timer += Time.deltaTime;
        
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
            if(!agent.weapons.weaponActive)
                agent.weapons.weaponActive = true;
        }
        if(!playerInSight)
        {
            agent.weapons.weaponActive = false;
        }
        
            
    }
    
    bool RaycastCheckIsPlayerInSight(AiAgent agent , float sqrDistancePfromA, float sqrShootingRange)
    {
        Ray ray = new Ray(agent.weapons.currentWeapon.shootingPoint.position,agent.weapons.currentWeapon.shootingPoint.forward);
        
        if(agent.weapons.showWeaponRange)
            Debug.DrawRay(ray.origin,ray.direction * agent.weapons.currentWeapon.shootingRange, Color.magenta);
        
        if (Physics.Raycast(ray, out RaycastHit hit, agent.weapons.currentWeapon.shootingRange))
        {
            if(hit.collider.GetComponent<PlayerController>()) { return true; }
            if(!hit.collider.GetComponent<PlayerController>()) { return false; }
        }
        
        if(sqrDistancePfromA < sqrShootingRange) { return true; }
        if(sqrDistancePfromA > sqrShootingRange) { return false; }
        
        return false;
    }
}
