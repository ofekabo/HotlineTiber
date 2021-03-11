using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR : Weapon
{
    private bool _holdingMouse;
    public override void Start()
    {
        base.Start();
        weaponID = 1;
        
    }

    public override void Fire()
    {
        if (weaponID == 3 && CheckAmmo())
        {
             if (Time.time > PNextFire)
             {
                 base.Fire();
                 Rigidbody bulletClone = Instantiate(bullet, shootingPoint.position, transform.rotation);
                 bulletClone.AddForce(transform.forward * bulletSpeed);
                
                
                ReduceAmmo();
                PNextFire = Time.time + fireRate;
            }
        }

    }
    
}


