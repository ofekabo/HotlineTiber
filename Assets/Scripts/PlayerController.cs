using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void Keycodes();
    public static event Keycodes keyPressed;
    private const string Horizontal = "Horizontal";

    private const string Vertical = "Vertical";
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Shooting")]
    [SerializeField] private GameObject Weapon;
    
    #region Private Refrences
    
    

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Aim();
        Shoot();
    }

    private void Move()
    {
        float horziontal = Input.GetAxis(Horizontal);
        float vertical = Input.GetAxis(Vertical);

        Vector3 movement = new Vector3(horziontal, 0, vertical);
        
        transform.Translate(movement * moveSpeed * Time.deltaTime,Space.World);
    }

    private void Aim()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            keyPressed?.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            // bool true for rifle continues shooting
        }

        if (Input.GetMouseButtonUp(0))
        {
            // bool false for rifle continues shooting (stop shooting)
        }
    }
}
