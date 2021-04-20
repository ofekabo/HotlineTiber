using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour
{
   
    public float maxHealth;
    
    public  SkinnedMeshRenderer _skinnedMesh;

    [Header("UI")] 
    private UIHealthBar healthBar;
    
    [Header("Read only")]
    public float currentHealth;

    [Header("HitEffect")] 
    public bool hitEffect = true;
    public float blinkIntensity;
    public float blinkDuration;
    public float _blinkTimer;
    public GameObject bloodVFX;
    
    // Start is called before the first frame update
    void Start()
    {
       
        _skinnedMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        currentHealth = maxHealth;
        
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidbodies) // going trough all rigidbodies and adding HitBox Script
        {
            HitBox hitBox = rb.gameObject.AddComponent<HitBox>();
            hitBox.health = this;
            if (hitBox.gameObject != gameObject)
            {
                hitBox.gameObject.layer = LayerMask.NameToLayer("Hitbox");
            }
        }

        OnStart();
    }


    public void TakeDamage(float damage,Vector3 direction,float force)
    {
        currentHealth -= damage;
        if(gameObject.CompareTag("Player"))
        {
            print(currentHealth);
        }
        
        if(healthBar)
            healthBar.SetHealthBarPrecentage(currentHealth / maxHealth);
        OnDamage(direction,force);
        if (currentHealth > 0)
        {
         
        }
        if (currentHealth <= 0.0f)
        {
            Die(direction,force);
            if(healthBar)
                healthBar.gameObject.SetActive(false);
        }
        
        _blinkTimer = blinkDuration;
    }

    private void Die(Vector3 direction,float force)
    {
        OnDeath(direction, force);
 
       
        
    }

    private void Update()
    {
        if (hitEffect)
        {
            _blinkTimer -= Time.deltaTime;
            float lerp = Mathf.Clamp01(_blinkTimer / blinkDuration);
            float intesnity = (lerp * blinkIntensity) +1f;
            foreach (var mat in _skinnedMesh.materials)
            {
                mat.color = Color.white * intesnity;
           
            } 
        }
    }

    public virtual void OnStart()
    {
        
    }
    public virtual void OnDeath(Vector3 direction,float force)
    {
        
    }
    public virtual void OnDamage(Vector3 direction,float force)
    {
        
    }

    public void SpawnBloodEffect(Vector3 pos)
    {
        GameObject bloodCloneVFX = Instantiate(bloodVFX,pos,quaternion.identity);
        Destroy(bloodCloneVFX,0.9f);
    }
    
}
