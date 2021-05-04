using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class HealthPickup : MonoBehaviour
{
    [SerializeField] int minHealthToAdd = 15;
    [SerializeField] int maxHealthToAdd = 35;

    private void Start()
    {
        // temp
       
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth pHealth = other.gameObject.GetComponent<PlayerHealth>();
        
        if(!pHealth) { return; }
        if(pHealth.currentHealth >= pHealth.maxHealth) { return; }
        
        pHealth.AddHealth(Random.Range(minHealthToAdd,maxHealthToAdd));
        Destroy(gameObject);
    }
}
