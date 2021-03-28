﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;

    }
    public bool isFiring = false;
    public float fireRate = 25;
    public float bulletSpeed = 1000f;
    public float bulletDrop = 0.0f;
    public float damage;
    public float weaponForce; 
    public float maxAmmo;
    public float initAmmo;
    
    public LayerMask layer;
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;
    public Transform shootingPoint;
    

    public string weaponName;

    private Ray ray;
    private RaycastHit hitInfo;
    private float accumlatedTime;
    private List<Bullet> bullets = new List<Bullet>();
    public float bulletLifeTime = 3f;


    public ActiveWeapon.WeaponSlot weaponSlot;
    public int weaponID;
    public float PNextFire;
    
    [Header("Read Only")]
    [SerializeField] float currentAmmo;
    public float CurrentAmmo { get => currentAmmo; }


    private bool _isPlayer;


    public enum WeaponType
    {
        RangeWeapon,
        MeleeWeapon
    }

    public WeaponType weaponType;
    
    private void Start()
    {
        currentAmmo = initAmmo;

        _isPlayer = transform.root.GetComponent<PlayerController>();
        
        if (weaponType == WeaponType.MeleeWeapon)
        {
            foreach (Transform t in transform.root)
            {
                if (t.CompareTag("PlayerMiddle"))
                    shootingPoint = t;
            }
        }
    }

    Vector3 GetPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) + (0.5f * gravity * bullet.time * bullet.time);
    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracer = Instantiate(tracerEffect,position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        Destroy(bullet.tracer.gameObject, bulletLifeTime);
        return bullet;
    }
    
    public void StartFiring()
    {
        isFiring = true;
        if (Time.time > PNextFire && currentAmmo >= 0)
        {
            FireBullet();
            PNextFire = Time.time + fireRate;
            currentAmmo--;
            if (_isPlayer)
                WeaponCameraShake();
           
        }
        
    }

    public void UpdateWeapon(float deltaTime)
    {
        // Need to keep track of cooldown even when not firing to prevent click spam.
        accumlatedTime += deltaTime;
        UpdateBullet(deltaTime);
    }
    
 

    public void UpdateBullet(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        bullets.RemoveAll(bullet => bullet.time > bulletLifeTime);
    }

    private void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    }



    private void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = (end - start).magnitude;
        ray.origin = start;
        ray.direction = direction;
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.blue, 3);

         if (Physics.Raycast(ray, out hitInfo,distance, layer))
            {
                hitEffect.transform.position = hitInfo.point;
                hitEffect.transform.forward = hitInfo.normal;
                hitEffect.Emit(1);
                bullet.tracer.transform.position = hitInfo.point;
                bullet.time = bulletLifeTime;
                

                var Prop = hitInfo.collider.GetComponent<Prop>();
                if (Prop)
                {
                    Prop.OnRaycastHit(this);
                }
                
                
                Rigidbody rb = hitInfo.collider.GetComponent<Rigidbody>();
                PlayerController pc = hitInfo.collider.GetComponent<PlayerController>();
                if (rb && !pc) 
                {
                    rb.AddForceAtPosition(ray.direction * weaponForce, hitInfo.point, ForceMode.Impulse);
                }
            
                var hitBox = hitInfo.collider.GetComponent<HitBox>();
                if (hitBox)
                {
                    hitBox.OnRaycastHit(this, ray.direction,weaponForce);
                }

                Debug.DrawRay(hitInfo.point, ray.direction * distance, Color.green, 1f);

                var radioScript = hitInfo.collider.GetComponent<RadioScript>();
                if (radioScript)
                {
                    radioScript.ReducePitch();
                }
            }

        
       
        if (bullet.tracer)
        {
            bullet.tracer.transform.position = end;
        }
       

    }
    
    
    private void FireBullet()
    {
        /*
         if(ammoCount <=0){
         return;
         }
         */
        foreach (var flash in muzzleFlash)
        {
            flash.Emit(1);
           
        }
        GameEvents.events.PlayGunShotID(weaponID);
        Vector3 velocity = shootingPoint.forward.normalized * bulletSpeed;
        
        var bullet = CreateBullet(shootingPoint.position,velocity);
        bullets.Add(bullet);
    }
    
    
    public void StopFiring()
    {
        isFiring = false;
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
    }

    void WeaponCameraShake()
    {
        GameEvents.events.PlayCameraShake();
    }
    
}
