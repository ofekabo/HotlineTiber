﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Random = System.Random;

public class AiWeapons : MonoBehaviour
{
    private AiAgent _aiAgent;
    [HideInInspector] public RaycastWeapon currentWeapon;
    public Animator rigController;
    public Rig aimingRig;
    public Transform weaponSlot;
    public Transform aiTarget;
    public bool weaponActive = false;
    [HideInInspector] public bool pickedUpWeapon; // not used
    public bool hasWeapon;


    public Transform meleeShootingPoint;
    
    void Awake()
    {
       
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            hasWeapon = true;
            EquipWeapon(existingWeapon);
        }
        if(!existingWeapon)
        {
            hasWeapon = false;
        }
        
    }

    void Start()
    {
        if (currentWeapon.weaponType == RaycastWeapon.WeaponType.MeleeWeapon)
        {
            currentWeapon.shootingPoint = meleeShootingPoint;
        }
    }

    private void Update()
    {
        if (aiTarget && currentWeapon && weaponActive)
        {
            currentWeapon.UpdateWeapon(Time.deltaTime);
            currentWeapon.StartFiring();
            if (currentWeapon.weaponType == RaycastWeapon.WeaponType.MeleeWeapon)
            {
                rigController.Play("hit_" + currentWeapon.weaponName);
            }
        }
        if(currentWeapon)
        {
            currentWeapon.StopFiring();
        }
    }

    

    public void EquipWeapon(RaycastWeapon weapon)
    {
        
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = weapon;
        currentWeapon.transform.SetParent(weaponSlot,false);
        currentWeapon.transform.localPosition = Vector3.zero;
        rigController.Play("hostler_" + weapon.weaponName);
        hasWeapon = true;
        StartCoroutine(SetWeaponRange(weapon));
    }

    IEnumerator SetWeaponRange(RaycastWeapon weapon)
    {
        yield return new WaitForSeconds(0.5f);
        if (weapon.weaponType == RaycastWeapon.WeaponType.MeleeWeapon)
        {
            GetComponent<AiAgent>().navMeshAgent.stoppingDistance = 2f;
        }
        if (weapon.weaponType == RaycastWeapon.WeaponType.RangeWeapon)
        {
            GetComponent<AiAgent>().navMeshAgent.stoppingDistance = 6f;
        }
        

    }

    public void DrawWeapon(bool drawWeapon)
    {
        rigController.SetBool("weaponPicked", drawWeapon);
    }

    public void SetWeightAiming(float weight)
    {
        aimingRig.weight = weight;
    }

    public void DropWeaon()
    {
        if (currentWeapon)
        {
            currentWeapon.transform.SetParent(null);
            currentWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.gameObject.AddComponent<Rigidbody>();
            currentWeapon = null;
        }
    }

    public bool HasWeapon()
    {
        return currentWeapon != null;
    }

    public Transform SetTarget(Transform target)
    {

        aiTarget.position = new Vector3(target.position.x, 1.5f, target.position.z);
        aiTarget.position += UnityEngine.Random.insideUnitSphere * currentWeapon.inAccuracy;
        return aiTarget;
    }
    
}
