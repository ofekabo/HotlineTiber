using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    [SerializeField]float tweenTime;
    [SerializeField] GameObject[] doorsPivots;

    void Start()
    {
        Invoke(nameof(OpenDoor),1f);
    }
    
    
    [ContextMenu("Open Door")]
    void OpenDoor()
    {
        foreach (var pivot in doorsPivots)
        {
            Vector3 pivotScale = pivot.transform.localScale;
            LeanTween.value(pivot,1,0.3f, tweenTime)
                .setEaseLinear()
                .setOnUpdate((value) =>
                {
                    pivot.transform.localScale = new Vector3(value,pivotScale.y,pivotScale.z);
                });
        }
    }
}
