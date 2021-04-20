using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    private RagdollController _ragdollController;
    private ActiveWeapon _activeWeapon;
    private PlayerController _pc;
    public override void OnStart()
    {
        _ragdollController = GetComponent<RagdollController>();
        _activeWeapon = GetComponent<ActiveWeapon>();
        _pc = GetComponent<PlayerController>();
    }
    public override void OnDeath(Vector3 direction,float force)
    {
        _ragdollController.ActivateRagdoll();
        _activeWeapon.DropWeapon();
        _pc.enabled = false;

    }
    public override void OnDamage(Vector3 direction,float force)
    {
        
    }
    
}
