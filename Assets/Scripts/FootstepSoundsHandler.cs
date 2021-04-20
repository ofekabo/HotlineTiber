using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSoundsHandler : MonoBehaviour
{
    private AudioSource _ac;
    [SerializeField] private AudioClip footstepRightClip;
    [SerializeField] private AudioClip footstepLeftClip;

    private void Start()
    {
        _ac = GetComponent<AudioSource>();
    }

    private void FootstepRight()
    {
        if (_ac.isPlaying) { return; }
        _ac.clip = footstepRightClip;
        _ac.Play();
    }
    private void FootstepLeft()
    {
        if (_ac.isPlaying) { return; }
        _ac.clip = footstepLeftClip;
        _ac.Play();
    }
    
}
