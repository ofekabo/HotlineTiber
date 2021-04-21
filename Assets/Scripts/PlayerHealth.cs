using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    private RagdollController _ragdollController;
    private ActiveWeapon _activeWeapon;
    private PlayerController _pc;
    private bool _isAlive;
    public bool IsAlive { get => _isAlive; }
    
    public static event Action PlayerDied;
    public override void OnStart()
    {
        _ragdollController = GetComponent<RagdollController>();
        _activeWeapon = GetComponent<ActiveWeapon>();
        _pc = GetComponent<PlayerController>();
        _isAlive = true;
    }
    
    
    public override void OnDeath(Vector3 direction,float force)
    {
        PlayerDied?.Invoke();
        _ragdollController.ActivateRagdoll();
        _activeWeapon.DropWeapon();
        _pc.enabled = false;
        _isAlive = false;
    }
    public override void OnDamage(Vector3 direction,float force)
    {
        
            print(currentHealth);
        
    }
    
    
}
