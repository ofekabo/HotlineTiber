using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ExplosiveProp : MonoBehaviour
{
    [Header("Effect")]
    [SerializeField] GameObject explosionEffect;
    [SerializeField] Transform explosionSpawnPoint;
    [Header("Explosion CFG")]
    [SerializeField] float radius;
    [SerializeField] float force = 700;
    [SerializeField] float damage = 6f;
   
    [Header("Debugging")]
    [SerializeField] bool showGizmos;
    [SerializeField][Range(0.2f,1f)] float alpha = 0.5f;
    


    public void Explode()
    {
        GameObject eplxosionSFXClone = Instantiate(explosionEffect,explosionSpawnPoint.position,transform.rotation);
        
        Collider[] _colliders = Physics.OverlapSphere(transform.position,radius);
        {
            foreach (var nearbyObject in _colliders)
            {
                Prop prop = nearbyObject.GetComponent<Prop>();
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                
                
                if (rb)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }

                if (prop)
                {
                    prop.GetDamage(damage);
                }
                
                AIHealth aiHealth = nearbyObject.GetComponent<AIHealth>();
                if (aiHealth)
                {
                    aiHealth.TakeDamage(damage * 2,transform.position - aiHealth.transform.position, force);
                }
                
                InnocentHealth innocentHealth = nearbyObject.GetComponent<InnocentHealth>();
                if (innocentHealth)
                {
                    innocentHealth.TakeDamage(damage * 4,transform.position - innocentHealth.transform.position, force);
                }
                
            }
            GameEvents.events.CallExplosionSound();
            Destroy(eplxosionSFXClone, 2.2f);
            Destroy(gameObject,2.2f);
        }
    }

    private void OnDrawGizmos()
    {
        if(!showGizmos) { return; }
        Gizmos.color = new Vector4(1,0,0,alpha);
        Gizmos.DrawSphere(transform.position,radius);
       
    }
}
