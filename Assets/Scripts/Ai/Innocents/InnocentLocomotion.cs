using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InnocentLocomotion : MonoBehaviour
{
    [HideInInspector]public Animator anim;
    private NavMeshAgent _navMesh;
    private float timer;
    [SerializeField] float generateAnimationCooldown = 2f;
    [SerializeField] int randomAnimationLength = 2;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (_navMesh.hasPath)
        {
            anim.SetFloat("Speed", _navMesh.velocity.magnitude);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }

    public void DelayedRandomAnimation()
    {
        Invoke("RandomBoolAnimation",0.5f);
    }
    
    private void RandomBoolAnimation()
    {
        float randomValue = Random.value;
        Debug.Log(randomValue);
        if (randomValue <= 0.5f)
        {
            return;
        }
        if (randomValue >= 0.5f)
        {
            RandomAnimation();
        }
    }

    public void GenerateAnimation()
    {
        timer += Time.deltaTime;
        if (timer > generateAnimationCooldown)
        {
            RandomAnimation();
            
            timer = 0;
        }
    }

    public void RandomAnimation()
    {
        int randomInt = Random.Range(0,randomAnimationLength);
        anim.SetInteger("RandomAnimation", randomInt);
    }
    
    // bool randomBool()
    // {
    //     float randomValue = UnityEngine.Random.value;
    //     if (randomValue <= 0.5f)
    //     {
    //         return true;
    //     }
    //     if (randomValue >= 0.5f)
    //     {
    //         return  false;
    //     }
    //     return randomBool();
    // }
}
