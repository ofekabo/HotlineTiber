using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponFab;
    public string weaponName;
    public int amountToAdd;
    AudioSource _ac;
    [SerializeField] AudioClip[] footstepsSounds;

    private void Start()
    {
        _ac = GetComponent<AudioSource>();
    }

    // AI
    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if(pc)
            _ac.PlayOneShot(footstepsSounds[Random.Range(0,footstepsSounds.Length)]);
        
        AiWeapons aiWeapon = other.gameObject.GetComponent<AiWeapons>();
        if (aiWeapon)
        {
            if(aiWeapon.hasWeapon) { return; }
            RaycastWeapon newWeapon = Instantiate(weaponFab);
            aiWeapon.EquipWeapon(newWeapon);
            aiWeapon.pickedUpWeapon = true;
            Destroy(gameObject);
        }
    }

    // Player
    private void OnTriggerStay(Collider other)
    {
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if (activeWeapon && activeWeapon.PickupWeapon)
        {
            if (!activeWeapon.weapon)
            {
                RaycastWeapon newWeapon = Instantiate(weaponFab);
                activeWeapon.Equip(newWeapon);
                GameEvents.events.CallUpdateAmmo();
            }
            
            if (activeWeapon.weapon.weaponName != weaponName)
            {
                RaycastWeapon newWeapon = Instantiate(weaponFab);
                activeWeapon.Equip(newWeapon);
                GameEvents.events.CallUpdateAmmo();
            }
            
            if(activeWeapon.weapon.weaponName == weaponName)
            {
                activeWeapon.weapon.AddAmmo(amountToAdd);
                GameEvents.events.CallUpdateAmmoDelayed(0.05f);
            }
            GameEvents.events.CallUpdateAmmo();
            
            Destroy(gameObject);
        }
    }
}
