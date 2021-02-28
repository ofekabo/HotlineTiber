﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] Transform[] shootingPoints;
    public override void Fire()
    {
        if (weaponID == 2 && CheckAmmo())
        {
            if (Time.time > PNextFire)
            {
                base.Fire();
                foreach(Transform spoint in shootingPoints)
                {
                    Rigidbody bulletClone = Instantiate(bullet, spoint.position, Quaternion.identity);
                    bulletClone.AddForce(transform.forward * bulletSpeed);
                } 
                ReduceAmmo();
                PNextFire = Time.time + fireRate;
            }
        }
        
    }

}
