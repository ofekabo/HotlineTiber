﻿using System;
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


    #region Events
    
    public static event Action UpdateInterface;
 

    private void UpdateUI()
    {
        UpdateInterface?.Invoke();
    }
    
    #endregion
    
    public override void OnStart()
    {
        _ragdollController = GetComponent<RagdollController>();
        _activeWeapon = GetComponent<ActiveWeapon>();
        _pc = GetComponent<PlayerController>();
        _isAlive = true;
    }
    
    
    public override void OnDeath(Vector3 direction,float force)
    {
        _ragdollController.ActivateRagdoll();
        _activeWeapon.DropWeapon();
        _pc.enabled = false;
        _isAlive = false;
    }
    public override void OnDamage(Vector3 direction,float force)
    {
       UpdateUI();
    }

    public void AddHealth(float healthToAdd)
    {
        currentHealth = Mathf.Min(currentHealth += healthToAdd,maxHealth);
        UpdateUI();
    }

  
}
