using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public override void Start()
    {
        base.Start();
        
    }

    public override void Fire()
    {
        if (weaponID == 2 && CheckAmmo())
        {
            if (Time.time > PNextFire)
            {
                base.Fire();

                Vector3 direction = transform.forward;
                Vector3 spread = new Vector3();

                Rigidbody bulletClone = Instantiate(bullet, shootingPoint.position, transform.rotation);
                bulletClone.AddForce(direction * bulletSpeed);
                for (int i = 0; i < amountOfBullets -1; i++)
                {
                    spread += transform.right * Random.Range(-1f, 1f);
                    spread += transform.up * Random.Range(-1f, 1f);

                    direction += spread.normalized * Random.Range(-0.15f, 0.15f);
                    bulletClone = Instantiate(bullet, shootingPoint.position, transform.rotation);
                    bulletClone.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
                    bulletClone.AddForce(direction * bulletSpeed);
                }

                ReduceAmmo();
                PNextFire = Time.time + fireRate;
            }
        }
    }
}