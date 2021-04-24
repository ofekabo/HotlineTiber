using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class InnocentIdleState : InnocentState
{
    public InnocentStateId GetId()
    {
        return InnocentStateId.Idle;
    }

    public void Enter(AiInnocent innocent)
    {
        innocent.locomotion.DelayedRandomAnimation();
    }

    public void Update(AiInnocent innocent)
    {
        
        
    }

    public void Exit(AiInnocent innocent)
    {
        
    }

   

  
}
