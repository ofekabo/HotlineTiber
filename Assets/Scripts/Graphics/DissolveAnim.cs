using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;


public class DissolveAnim : MonoBehaviour
{
    
    
    private SkinnedMeshRenderer _skinnedMesh;
    private Material _mat;
    [SerializeField] float tweenTime = 2f;
    
    

    private void Start()
    {
        _skinnedMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        _mat = _skinnedMesh.material;
        
    }


    [ContextMenu("Activate Death")]
    public void Death()
    {
        LeanTween.value(0, 1f, tweenTime).setOnUpdate((value) =>
        {
            _mat.SetFloat("_DissolveAmount",value);
        });
    }
}