using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : MonoBehaviour
{
    [Header("Ammo")] //
    public int startingAmmo;

    public int maxAmmo;
    public int _currentAmmo;

    [Header("Fire Rate")] //
    public float fireRate;

    [Tooltip("USED ONLY FOR SHOTGUN")] public int amountOfBullets;
    protected float PNextFire;

    [Header("Bullet")] public Rigidbody bullet;
    public Transform shootingPoint;
    public float bulletSpeed;

    [Header("Weapon ID")] public int weaponID;

    public int ammoWepID;


    private void OnEnable()
    {
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        PlayerWeapons.ChooseWepDel += SetWeaponID;
        SetShootingPoints();
        _currentAmmo = startingAmmo;
        PlayerController.shootPressed += Fire;
        GameEvents.events.onGunPickupTrigger += RefillAmmo;
    }

    public virtual void Update()
    {
        _currentAmmo = Mathf.Clamp(_currentAmmo, 0, maxAmmo);
    }


    public delegate void CameraShakeDel();

    public static event CameraShakeDel activateCameraShake;

    public virtual void Fire()
    {
        activateCameraShake?.Invoke();
    }

    public bool CheckAmmo()
    {
        if (_currentAmmo > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ReduceAmmo()
    {
        _currentAmmo--;
    }

    private void RefillAmmo(int wepID, int ammo)
    {
        if (ammoWepID == wepID)
            _currentAmmo += ammo;
    }


    private void SetWeaponID(int wepid)
    {
        weaponID = wepid;
    }

    // sets the shooting point automatically
    // for each weapon 1 shooting point as the first child!
    public virtual void SetShootingPoints()
    {
        shootingPoint = this.gameObject.transform.GetChild(0).transform;
    }

    private void OnDisable()
    {
    }
}