using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponFab;
    [SerializeField] private string weaponName;
    [SerializeField] private int amountToAdd;

    // AI
    private void OnTriggerEnter(Collider other)
    {
        AiWeapons aiWeapon = other.gameObject.GetComponent<AiWeapons>();
        if (aiWeapon)
        {
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
        if (activeWeapon && activeWeapon.PickupWeapon )
        {
            if(activeWeapon.weapon.weaponName == weaponName)
            {
                activeWeapon.weapon.AddAmmo(amountToAdd);
                Destroy(gameObject);
            }

            if (activeWeapon.weapon.weaponName != weaponName)
            {
                RaycastWeapon newWeapon = Instantiate(weaponFab);
                activeWeapon.Equip(newWeapon);
                
            }
            
        }

        
    }
}
