using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ExplosiveProp : MonoBehaviour
{
    [Header("Explosion CFG")]
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float radius;
    [SerializeField] float force = 700;
    [SerializeField] float damage = 6f;
   
    [Header("Debugging")]
    [SerializeField] bool showGizmos;
    [SerializeField][Range(0.2f,1f)] float alpha = 0.5f;
    
    public void Explode()
    {
        GameObject eplxosionSFXClone = Instantiate(explosionEffect,transform.position,transform.rotation);
        
        Collider[] _colliders = Physics.OverlapSphere(transform.position,radius);
        {
            foreach (var nearbyObject in _colliders)
            {
                Prop prop = nearbyObject.GetComponent<Prop>();
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                AIHealth aiHealth = nearbyObject.GetComponent<AIHealth>();
                if (rb)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }

                if (prop)
                {
                    prop.GetDamage(damage);
                }

                if (aiHealth)
                {
                    aiHealth.TakeDamage(damage * 2,transform.position - aiHealth.transform.position, force);
                }
                
            }

            Destroy(eplxosionSFXClone, 1.5f);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if(!showGizmos) { return; }
        Gizmos.color = new Vector4(1,0,0,alpha);
        Gizmos.DrawSphere(transform.position,radius);
       
    }
}
