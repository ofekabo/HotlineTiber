using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    [Tooltip("Time to wait till checking distance again")]
    public float maxTime = 1f;
    [Tooltip("Used for resseting ai movement , once player has reached more than *MaxDistance* the Agent chases him again")]
    public float maxDistance = 1f;
    [Tooltip("View distance from agent to player")]
    public float viewDistance = 10f;


}
