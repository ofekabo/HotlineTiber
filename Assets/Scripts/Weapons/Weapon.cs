using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // firing
    [Header("Ammo")]
    public int startingAmmo;
    public int maxAmmo;
    private int _ammo;
    [Header("Fire Rate")]
    public float fireRate;
    protected float PNextFire;
    
    [Header("Bullet")]
    public Rigidbody bullet;
    public Transform shootingPoint;
    public float bulletSpeed;
    
    [Header("Weapon ID")]
    public int weaponID;
    

    
    

    // Start is called before the first frame update
    public virtual void Start()
    {
        _ammo = startingAmmo;
        PlayerWeapons.ChooseWepDel += SetWeaponID;
        PlayerController.shootPressed += Fire;
      
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
        
    }

    public virtual void Fire() { }

    


    public void ReduceAmmo()
    {
        _ammo--;
    }

    public void RefillAmmo()
    {
        _ammo = maxAmmo;
    }
    

    private void SetWeaponID(int wepid)
    {
        weaponID = wepid;
        
    }

    

}
