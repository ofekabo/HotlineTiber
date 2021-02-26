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
                Rigidbody bulletClone = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
                bulletClone.velocity = transform.forward * bulletSpeed;
                
                
                ReduceAmmo();
                PNextFire = Time.time + fireRate;
            }
        }

    }

}


