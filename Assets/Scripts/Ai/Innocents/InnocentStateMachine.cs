using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnocentStateMachine
{
   public InnocentState[] states;
   public AiInnocent ino;
   public InnocentStateId currentState;
   
   public InnocentStateMachine(AiInnocent innocent)
   {
      this.ino = innocent;
      int numStates = System.Enum.GetNames(typeof(AiStateId)).Length;
      states = new InnocentState[numStates];
   }

   public void RegisterState(InnocentState state)
   {
      int index = (int)state.GetId();
      states[index] = state;
   }

   public InnocentState GetState(InnocentStateId stateId)
   {
      int index = (int)stateId;
      return states[index];
   }
   
   public void Update()
   {
      GetState(currentState)?.Update(ino);
   }

   public void ChangeState(InnocentStateId newState)
   {
      GetState(currentState)?.Exit(ino);
      currentState = newState;
      GetState(currentState)?.Enter(ino);
   }
}
