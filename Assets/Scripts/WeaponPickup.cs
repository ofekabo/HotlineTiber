using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script access the PlayerWeapons and if u step on any of them
// it gives access to the weapon depends on the weapon u stepped on
public class WeaponPickup : MonoBehaviour
{
    private PlayerWeapons _playerWep;
    private void Start()
    {
        _playerWep = FindObjectOfType<PlayerWeapons>(); // need to find better solution
        if(_playerWep == null)
            Debug.Log("No player could founud (WeaponPickup.cs)");
    }

    public enum weaponEnum // can select in inspector which weapon it is.
    {
        Pistol,
        Shotgun,
        AR
    };

    public weaponEnum dropDown;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            returnWeaponID();
            Destroy(gameObject, 0.1f);
        }
    }

    private void returnWeaponID()
    {
        if (dropDown == weaponEnum.Pistol)
        {
            //does nothing atm , if we want to start with walking only or maybe add melee.
        }

        if (dropDown == weaponEnum.Shotgun)
        {
            if(_playerWep != null)
                _playerWep.shotgunUnlocked = true;
        }

        if (dropDown == weaponEnum.AR)
        {
            if(_playerWep != null)
                _playerWep.arUnlocked = true;
        }
        
    }
}
