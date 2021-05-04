using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;


public class DissolveAnim : MonoBehaviour
{
    private SkinnedMeshRenderer _skinnedMesh;
    private MeshRenderer _meshRenderer;
    [SerializeField] float delayToAir = 2.5f;
    [SerializeField] float tweenTime = 2f;
    [SerializeField] float liftTime = 2f;
    public float TweenTime { get=> tweenTime; }
    
    

    private void Start()
    {
        try
        {
            _skinnedMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        }
        catch (NullReferenceException e)
        {
           
        }

        
        
        try
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
        }
        catch (NullReferenceException e)
        {
            
        }
       
    }


    public IEnumerator LiftInAir(Rigidbody rb)
    {
        yield return new WaitForSeconds(delayToAir);
        rb.isKinematic = true;
        LeanTween.moveY(gameObject,2f,liftTime).setEaseInCubic();
        yield return new WaitForSeconds(liftTime);
        DissolveDeath();
        yield return new WaitForSeconds(tweenTime);
        LeanTween.cancel(gameObject);
        Destroy(gameObject,0.5f);
    }
    
    [ContextMenu("Activate Death")]
    public void DissolveDeath()
    {
        LeanTween.value(0, 1f, tweenTime).setOnUpdate((value) =>
        {
            if(_skinnedMesh)
                _skinnedMesh.material.SetFloat("_DissolveAmount",value);
            if(_meshRenderer)
                _meshRenderer.material.SetFloat("_DissolveAmount",value);
            
        });
    }
}