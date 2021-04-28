using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateId initState;
    public AiAgentConfig config;
    
    [HideInInspector] public AiStateMachine stateMachine;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Transform playerTransform;
    [HideInInspector] public RagdollController ragdoll;
    [HideInInspector] public UIHealthBar healthBar;
    [HideInInspector] public AiWeapons weapons;

    public CapsuleCollider capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
     void Update()
     {
         stateMachine.Update();
     }
}
