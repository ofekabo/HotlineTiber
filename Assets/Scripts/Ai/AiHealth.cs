using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHealth : MonoBehaviour
{
   
    public float maxHealth;

    private AiAgent agent;
    private SkinnedMeshRenderer _skinnedMesh;

    [Header("UI")] 
    private UIHealthBar healthBar;
    
    [Header("Read only")]
    public float currentHealth;

    [Header("HitEffect")]
    public float blinkIntensity;
    public float blinkDuration;
    private float _blinkTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<AiAgent>();
        _skinnedMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        currentHealth = maxHealth;
        
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidbodies) // going trough all rigidbodies and adding HitBox Script
        {
            HitBox hitBox = rb.gameObject.AddComponent<HitBox>();
            hitBox.aiHealth = this;
        }
    }


    public void TakeDamage(float damage,Vector3 direction)
    {
        currentHealth -= damage;
        healthBar.SetHealthBarPrecentage(currentHealth / maxHealth);
        if (currentHealth <= 0.0f)
        {
            Die(direction);
            healthBar.gameObject.SetActive(false);
        }

        _blinkTimer = blinkDuration;
    }

    private void Die(Vector3 direction)
    {
        AiDeathState deathState = agent.stateMachine.GetState(AiStateId.Death) as AiDeathState;
        deathState.direction = direction;
        agent.stateMachine.ChangeState(AiStateId.Death);
    }

    private void Update()
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
