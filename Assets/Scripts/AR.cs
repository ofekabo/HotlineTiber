using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR : Weapon
{
    public override void ARFire()
    {
        if (Time.time > nextfire)
        {
            Rigidbody bulletClone = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
            bulletClone.velocity = transform.forward * bulletSpeed;
            ReduceAmmo();
            nextfire = Time.time + firerate;
        }
    }

}
