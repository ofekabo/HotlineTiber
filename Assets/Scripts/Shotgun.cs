using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] Transform[] shootingPoints;
    public override void ShotgunFire()
    {
        if (Time.time > nextfire)
        {
            foreach(Transform spoint in shootingPoints)
            {
                Rigidbody bulletClone = Instantiate(bullet, spoint.position, Quaternion.identity);
                bulletClone.velocity = transform.forward * bulletSpeed;
            } 
            ReduceAmmo();
            nextfire = Time.time + firerate;
        }
    }

}
