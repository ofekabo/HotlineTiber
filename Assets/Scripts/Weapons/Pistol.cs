﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol :Weapon
{

    public override void Fire()
    {
        if (weaponID == 1)
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
