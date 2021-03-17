﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiLocomotion : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

   
    void Update()
    {
        if (agent.hasPath)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
        
    }
}
