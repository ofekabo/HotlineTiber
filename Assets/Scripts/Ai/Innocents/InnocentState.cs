using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum InnocentStateId
{
   Idle,
   Run,
   Death
}

public interface InnocentState
{
   InnocentStateId GetId();
   void Enter(AiInnocent innocent);
   void Update(AiInnocent innocent);
   void Exit(AiInnocent innocent);
}

