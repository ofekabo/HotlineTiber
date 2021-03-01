using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol :Weapon
{

    public override void Fire()
    {
        
        if (weaponID == 1 && CheckAmmo())
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
