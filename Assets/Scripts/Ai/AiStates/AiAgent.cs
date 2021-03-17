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
    public Transform playerTransform;

    public RagdollController ragdoll;
    public UIHealthBar healthBar;
    

    // Start is called before the first frame update
    void Start()
    {
        if ((playerTransform == null))
        {
            playerTransform = FindObjectOfType<PlayerController>().transform;
        }
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        ragdoll = GetComponentInChildren<RagdollController>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        
        // registering states
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new AiIldeState());
        stateMachine.RegisterState(new AiFindWeaponState());
        
        stateMachine.ChangeState(initState);
    }

    // Update is called once per frame
     void Update()
     {
         stateMachine.Update();
     }
}
