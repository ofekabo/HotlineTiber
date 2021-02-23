using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private Transform player;

    // [SerializeField] Transform middlePosition;
    //
    // [SerializeField] private Vector3 offset;
    // // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;

    }

    //
    // // Update is called once per frame
    // void Update()
    // {
    //     if (middlePosition == null)
    //     {
    //         Debug.LogError(("Assign MiddlePoint from the player to the Camera"));
    //     }
    //     else
    //     {
    //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //         RaycastHit hit;
    //         if (Physics.Raycast(ray, out hit, Mathf.Infinity)) ;
    //         {
    //             middlePosition.transform.position = player.position + hit.point / 2;
    //
    //         }
    //         Debug.DrawRay(ray.origin, ray.direction * 15, Color.red);
    //
    //
    //         float midPosX = Mathf.Clamp(middlePosition.position.x,  -5,
    //             5);
    //         float midPosZ = Mathf.Clamp(middlePosition.position.z,  -5,
    //             5);
    //
    //         middlePosition.position = new Vector3(midPosX, 1, midPosZ);
    //         
    //
    //         transform.position =
    //             Vector3.Lerp(transform.position, middlePosition.position + offset, 3f * Time.deltaTime);
    //     }
    // }
    [SerializeField] float damping = 12.0f;
    [SerializeField] float height = 11.0f;
    [SerializeField] float offset = 0f;

    private Vector3 _center;
    [SerializeField] float viewDistance = 3.0f;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = viewDistance;
        Vector3 CursorPosition = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 PlayerPosition = player.position;

        _center = new Vector3((PlayerPosition.x + CursorPosition.x) / 2, PlayerPosition.y,
            (PlayerPosition.z + CursorPosition.z) / 2);
    
        transform.position = Vector3.Lerp(transform.position, _center + new Vector3(0, height, offset),
            Time.deltaTime * damping);
    }
}
