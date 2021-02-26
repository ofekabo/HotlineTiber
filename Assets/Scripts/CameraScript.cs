using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private Transform player;
    
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;

    }

    [SerializeField] float damping = 12.0f;
    [SerializeField] float height = 11.0f;
    [SerializeField] float offset = 0f;

    private Vector3 _center;
    [SerializeField] float viewDistance = 3.0f;

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
