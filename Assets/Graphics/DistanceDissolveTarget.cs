using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class DistanceDissolveTarget : MonoBehaviour
{
    [SerializeField] Transform objectToTrack = null;
    
    private Material _material;
    private Renderer _renderer;

    public Renderer newRenderer
    {
        get
        {
            if (_renderer == null)
            {
                _renderer = GetComponent<Renderer>();
            }
            return _renderer;
        }
    }

    public Material newMaterial
    {
        get
        {
            if (_material == null)
            {
                _material = newRenderer.material;
            }
            return _material;
        }
    }
    
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
    }

    void Update()
    {
        if(objectToTrack == null) { return; }
        newMaterial.SetVector("_Position", objectToTrack.position);
    }

    private void OnDestroy()
    {
        _renderer = null;
        if(_material == null) { return; }
        Destroy(_material);
        _material = null;
    }
}
