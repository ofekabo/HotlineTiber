using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float firerate;

   protected float nextfire;

    public int startingAmmo;

    private int ammo;

    public int maxAmmo;

    public bool automaticShoot;

    public Rigidbody bullet;

    public Transform shootingPoint;

    public float bulletSpeed;

    private int weaponid;

    // Start is called before the first frame update
    public virtual void Start()
    {
       
        ammo = startingAmmo;
        PlayerWeapons.ChooseWepDel += SetWeaponID;

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (weaponid == 1)
        {
            ClearListeners();
            PlayerController.keyPressed += PistolFire;

        }

        if (weaponid == 2)
        {
            ClearListeners();
            PlayerController.keyPressed += ShotgunFire;
        }

        if (weaponid == 3)
        {
            ClearListeners();
            PlayerController.keyPressed += ARFire;
        }
    }

    public virtual void PistolFire() { }

    public virtual void ShotgunFire() { }
   
    public virtual void ARFire() { }
    


    public void ReduceAmmo()
    {
        ammo--;
    }

    public void RefillAmmo()
    {
        ammo = maxAmmo;
    }

    private void SetWeaponID(int wepid)
    {
        weaponid = wepid;
        Debug.Log(weaponid);
    }

    private void ClearListeners()
    {
        PlayerController.keyPressed -= PistolFire;
        PlayerController.keyPressed -= ShotgunFire;
        PlayerController.keyPressed -= ARFire;
        Debug.Log("clear");
    }

}
