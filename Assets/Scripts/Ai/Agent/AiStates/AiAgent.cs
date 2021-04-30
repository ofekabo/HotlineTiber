using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] bool showViewRange = false;
    [SerializeField][Range(0.3f,0.8f)] float alpha = 0.3f;
    
    [Header("State Machine")]
    public AiStateId initState;
    public AiAgentConfig config;
    [HideInInspector] public DissolveAnim dissolveAnim;
    [HideInInspector] public AiStateMachine stateMachine;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Transform playerTransform;
    [HideInInspector] public RagdollController ragdoll;
    [HideInInspector] public UIHealthBar healthBar;
    [HideInInspector] public AiWeapons weapons;
    [HideInInspector] public CapsuleCollider capsuleCollider;
        
    public float timerCooldown;
    

  
    private PlayerHealth _playerHealth;
   

    bool _flagIsAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        dissolveAnim = GetComponent<DissolveAnim>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
        if (playerTransform == null)
        {
            playerTransform = FindObjectOfType<PlayerController>().transform;
        }
            
        capsuleCollider = GetComponent<CapsuleCollider>();
        
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        ragdoll = GetComponentInChildren<RagdollController>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        weapons = GetComponent<AiWeapons>();
        
        // registering states
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiFindWeaponState());
        stateMachine.RegisterState(new AiAttackPlayerState());
        
        stateMachine.ChangeState(initState);
        
        _playerHealth = playerTransform.GetComponent<PlayerHealth>();
        _flagIsAlive = true;
    }
    
    
    
    void Update()
    {
        stateMachine.Update();
        
        if (!_playerHealth) { return; }

        
        // handles player death
        if (!_playerHealth.IsAlive && _flagIsAlive)
        {
            PlayerDeathHandler();
            _flagIsAlive = false;
        }
    }

     void PlayerDeathHandler()
     {
         stateMachine.ChangeState(AiStateId.Death); // cause why not
     }

     private void OnDrawGizmos()
     {
         if (showViewRange)
         {
             Gizmos.color = new Vector4(1,0,0,alpha);
             Gizmos.DrawCube(transform.position,Vector3.one * 2 * config.viewDistance);
         }
         
     }
}
