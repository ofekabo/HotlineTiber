using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR : Weapon
{
    private bool _holdingMouse;
    public override void Start()
    {
        base.Start();
    }

    public override void Fire()
    {
        if (weaponID == 3)
        {
             if (Time.time > PNextFire)
             {
                 base.Fire();
                 Rigidbody bulletClone = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
                bulletClone.AddForce(transform.forward * bulletSpeed);
                
                
                ReduceAmmo();
                PNextFire = Time.time + fireRate;
            }
        }

    }
    
}


