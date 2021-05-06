using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateMachine
{
  public AiState[] states;
  public AiAgent agent;
  public AiStateId currentState;

  #region delay
  private float timer;
  private float delay = 0.8f;
  

  #endregion

  public AiStateMachine(AiAgent agent)
  {
      this.agent = agent;
      int numStates = System.Enum.GetNames(typeof(AiStateId)).Length;
      states = new AiState[numStates];
  }

  public void RegisterState(AiState state)
  {
      int index = (int)state.GetId();
      states[index] = state;
  }

  public AiState GetState(AiStateId stateId)
  {
      int index = (int)stateId;
      return states[index];
  }
  public void Update()
  {
      timer += Time.deltaTime;
      if (timer > delay)
      {
          if (agent.playerTransform.GetComponent<ActiveWeapon>().isHolstered)
          {
              if(agent.flagTookOutWep == false) { return; }
          
          }
          else
          {
              agent.flagTookOutWep = true;
          }
      }


      if (agent.flagTookOutWep)
      {
          GetState(currentState)?.Update(agent);
      }
      
  }

  public void ChangeState(AiStateId newState)
  {
      GetState(currentState)?.Exit(agent);
      currentState = newState;
      GetState(currentState)?.Enter(agent);
  }

  void CheckPlayerHostlerState()
  {
      
  }
}
