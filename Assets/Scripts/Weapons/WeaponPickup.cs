using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// this script access the PlayerWeapons and if u step on any of them
// it gives access to the weapon depends on the weapon u stepped on
public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private int minAmmo,maxAmmo;
    private PlayerWeapons _playerWep;
    
    private int _weaponID;
  public enum weaponEnum // can select in inspector which weapon it is.
    {
        Pistol,
        Shotgun,
        AR
    };

    public weaponEnum weapon;
    private void Start()
    {
        _playerWep = FindObjectOfType<PlayerWeapons>(); // need to find better solution
        if (_playerWep == null)
            Debug.Log("No player could found (WeaponPickup.cs)");
        if (weapon == weaponEnum.Pistol) _weaponID = 1;
        if (weapon == weaponEnum.Shotgun) _weaponID = 2;
        if (weapon == weaponEnum.AR) _weaponID = 3;
    }

  

    
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
        if (weapon == weaponEnum.Pistol)
        {
            //does nothing atm , if we want to start with walking only or maybe add melee.
        }

        if (weapon == weaponEnum.Shotgun)
        {
            if (_playerWep != null)
            {
                if (_playerWep.shotgunUnlocked == false)
                    _playerWep.shotgunUnlocked = true;
                GameEvents.events.RefillAmmo(_weaponID,AmmoToRefill());
            }
        }

        if (weapon == weaponEnum.AR)
        {
            if (_playerWep != null)
            {
                if (_playerWep.arUnlocked == false)
                    _playerWep.arUnlocked = true;
                GameEvents.events.RefillAmmo(_weaponID,AmmoToRefill());
            }
        }
    }

    private int AmmoToRefill()
    {
        return Random.Range(minAmmo, maxAmmo);
    }
}