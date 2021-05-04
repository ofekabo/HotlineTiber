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
    
    
    [Header("Death Effect")]
    private DissolveAnim _dissolveAnim;
    private bool _isDead;
    public DissolveAnim DissolveAnim { get => _dissolveAnim; }
    // Start is called before the first frame update
    void Start()
    {
       
        _skinnedMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        currentHealth = maxHealth;
        _isDead = false;

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
        _dissolveAnim = GetComponent<DissolveAnim>();
        
    }


    public void TakeDamage(float damage,Vector3 direction,float force)
    {
        currentHealth -= damage;
       
        
        if(healthBar)
            healthBar.SetHealthBarPrecentage(currentHealth / maxHealth);
        OnDamage(direction,force);
        if (currentHealth > 0)
        {
         
        }
        if (currentHealth <= 0.0f)
        {
            _isDead = true;
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
        if (!hitEffect) { return; }
        
        _blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(_blinkTimer / blinkDuration);
        float intesnity = (lerp * blinkIntensity) +1f;
        foreach (var mat in _skinnedMesh.materials)
        {
            mat.SetColor("_Albedo", Color.white * intesnity);
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

    public void DeathDissolveAnimation()
    {
        if(!_isDead) { return; }
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidbodies) // going trough all rigidbodies and adding HitBox Script
        {
            StartCoroutine((_dissolveAnim.LiftInAir(rb)));
        }
    }
    
}
