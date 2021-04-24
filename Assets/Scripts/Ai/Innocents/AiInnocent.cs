using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiInnocent : MonoBehaviour
{
   public InnocentStateId initState;
   [HideInInspector] public InnocentStateMachine stateMachine;
   [HideInInspector] public NavMeshAgent navMeshAgent;
   [HideInInspector] public CapsuleCollider capsuleCollider;
   [HideInInspector] public RagdollController ragdollController;
   [HideInInspector] public InnocentLocomotion locomotion;

   private void Start()
   {
      
      stateMachine = new InnocentStateMachine(this);
      ragdollController = GetComponent<RagdollController>();
      capsuleCollider = GetComponent<CapsuleCollider>();
      navMeshAgent = GetComponent<NavMeshAgent>();
      locomotion = GetComponent<InnocentLocomotion>();
      
      
      
      stateMachine.RegisterState(new InnocentIdleState());
      stateMachine.RegisterState(new InnocentDeathState());
      
      stateMachine.ChangeState(initState);
   }

   private void Update()
   {
      stateMachine.Update();
   }
}
