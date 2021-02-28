using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Tooltip("damping is the move speed the camera follows the mouse , the higher the faster.")]
    [SerializeField] float damping = 12.0f;
    [Tooltip("height is the offset of the camera from the player in Y axis.")]
    [SerializeField] float height = 11.0f;
    [Tooltip("offset is Z offset for the camera , more forward or backwards.")]
    [SerializeField] float offset = 0f;
    [Tooltip("change view distance to get farther sight with mouse.")]
    [SerializeField] float viewDistance = 3.0f;
    private Vector3 _center;
    
    
    private Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

 
    

    void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = viewDistance;
        Vector3 CursorPosition = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 PlayerPosition = player.position;

        _center = new Vector3((PlayerPosition.x + CursorPosition.x) * 0.5f, PlayerPosition.y,
            (PlayerPosition.z + CursorPosition.z) * 0.5f);
    
        transform.position = Vector3.Lerp(transform.position, _center + new Vector3(0, height, offset),
            Time.deltaTime * damping);
    }


    
}
