using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InnocentLocomotion : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent _navMesh;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        
    }

    public void DelayedRandomAnimation()
    {
        Invoke("RandomAnimation",0.5f);
    }
    
    private void RandomAnimation()
    {
        float randomValue = UnityEngine.Random.value;
        if (randomValue <= 0.5f)
        {
            anim.SetTrigger("Talking");
        }
        if (randomValue >= 0.5f)
        {
            return ;
        }
        
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
