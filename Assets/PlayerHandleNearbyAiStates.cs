using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandleNearbyAiStates : MonoBehaviour
{
    
    [SerializeField]float boxSize;
    [SerializeField][Range(0.2f,0.8f)] float alpha = 0.2f;
    [SerializeField] bool drawGizmos;
    [SerializeField] Vector3 offset;
    
    private ActiveWeapon playerWeapon;

    void Start()
    {
        playerWeapon = transform.root.GetComponent<ActiveWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerWeapon.weapon.isFiring) { return; }
        
        Collider[] hits = (Physics.OverlapBox(transform.position, new Vector3(boxSize, 1.5f, boxSize), transform.rotation));
        foreach (var hit in hits)
        {
            AiAgent agent = hit.GetComponent<AiAgent>();
            AiInnocent inno = hit.GetComponent<AiInnocent>();
            
            
            if (agent && agent.stateMachine.currentState != AiStateId.AttackPlayer)
            {
                agent.stateMachine.ChangeState(AiStateId.AttackPlayer);
            }
            if (inno && inno.stateMachine.currentState != InnocentStateId.Run)
            {
                inno.stateMachine.ChangeState(InnocentStateId.Run);
            }
        }
               
    }
    
    private void OnDrawGizmos()
    {
        if(!drawGizmos) { return; }
        Gizmos.color = new Vector4(1, 0, 0, alpha);
        Gizmos.DrawCube(transform.position + offset, new Vector3(boxSize, 5, boxSize));
        
    }
}
