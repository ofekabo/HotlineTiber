﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public int damage;
    [SerializeField] private float bulletHeight = 2f;
    [SerializeField] private float bulletSpeed = 80f;
    [SerializeField] private float bulletDetectionRadius = 0.5f;
    [SerializeField] private GameObject particleEffect;


    //bullet raycasting
    private Vector3 _previousPos;


    // Start is called before the first frame update
    void Start()
    {
        _previousPos = transform.position;
        Destroy(gameObject,4f);
    }

    // Update is called once per frame
    void Update()
    {
        _previousPos = transform.position;
        
        if (!Input.GetKey(KeyCode.LeftShift)) // if not pressing using fixed bullet height
            transform.position = new Vector3(transform.position.x, bulletHeight, transform.position.z);
        
        transform.Translate(transform.forward * bulletSpeed * Time.deltaTime,Space.World);
        RaycastHit[] hits= Physics.SphereCastAll(new Ray(_previousPos, (transform.position - _previousPos).normalized),bulletDetectionRadius,
            (transform.position - _previousPos).magnitude);

        foreach (RaycastHit hit in hits)
        {

          
            if(hit.collider.CompareTag("Prop"))
            {
                if (hit.collider.GetComponent<Prop>() != null)
                {
                    hit.collider.GetComponent<Prop>().RecDamage(damage);
                    hit.collider.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(4f,8f), ForceMode.Impulse);
                }
                
                Destroy(gameObject);
                SpawnHitVFX();
            }
            
            
            if (hit.collider.CompareTag("Static"))
            {
                SpawnHitVFX();
                Destroy(gameObject);
            }

            HitBox hitBox = hit.collider.GetComponent<HitBox>();
            Ray ray = new Ray(transform.position, transform.forward);
            if (hitBox)
            {
                hitBox.ONSphereCastHit(this,ray.direction);
                SpawnHitVFX();
                Destroy(gameObject);
            }

        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, bulletDetectionRadius);
    }

    private void SpawnHitVFX()
    {
        GameObject hitVFX = Instantiate(particleEffect, transform.position, Quaternion.identity);
        Destroy(hitVFX, 0.8f);
    }
}
