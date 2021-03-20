using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Random = System.Random;

public class AiWeapons : MonoBehaviour
{
    [HideInInspector] public RaycastWeapon currentWeapon;
    public Animator rigController;
    public Rig aimingRig;
    public Transform weaponSlot;
    public Transform aiTarget;
    public float inAccuracy;
    public bool weaponActive = false;
    [HideInInspector] public bool pickedUpWeapon;

    [Header("Randomness Rate")]
    public float randomRate;
    public float nextRandom;

    void Start()
    {
        
    }

    private void Update()
    {
        if (aiTarget && currentWeapon && weaponActive)
        {
            currentWeapon.UpdateWeapon(Time.deltaTime);
            currentWeapon.StartFiring();
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
        aiTarget.position += UnityEngine.Random.insideUnitSphere * inAccuracy;
        return aiTarget;
    }
    
}
