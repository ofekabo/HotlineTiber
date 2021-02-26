using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] Transform[] shootingPoints;
    public override void Fire()
    {
        if (weaponID == 2)
        {
            if (Time.time > PNextFire)
            {
                foreach(Transform spoint in shootingPoints)
                {
                    Rigidbody bulletClone = Instantiate(bullet, spoint.position, Quaternion.identity);
                    bulletClone.velocity = transform.forward * bulletSpeed;
                } 
                ReduceAmmo();
                PNextFire = Time.time + fireRate;
            }
        }
        
    }

}
