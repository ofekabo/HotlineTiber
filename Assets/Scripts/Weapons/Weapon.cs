using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // firing
    [Header("Ammo")] public int startingAmmo;
    public int maxAmmo;
    public int _currentAmmo;
    [Header("Fire Rate")] public float fireRate;
    protected float PNextFire;

    [Header("Bullet")] public Rigidbody bullet;
    public Transform shootingPoint;
    public float bulletSpeed;

    [Header("Weapon ID")] public int weaponID;


    // Start is called before the first frame update
    public virtual void Start()
    {
        _currentAmmo = startingAmmo;
        PlayerWeapons.ChooseWepDel += SetWeaponID;
        PlayerController.shootPressed += Fire;
    }

    // Update is called once per frame
    public virtual void Update()
    {
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

    public void RefillAmmo()
    {
        _currentAmmo = maxAmmo;
    }


    private void SetWeaponID(int wepid)
    {
        weaponID = wepid;
    }
}