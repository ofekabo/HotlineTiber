using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const int AR = 3;
    private const string Horizontal = "Horizontal";

    private const string Vertical = "Vertical";
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    
    
    
    #region Private Refrences

    private Rigidbody _rb;
    private Animator _anim;
    private PlayerWeapons _playerWeapons;
    

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerWeapons = GetComponent<PlayerWeapons>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Shoot();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horziontal = Input.GetAxis(Horizontal);
        float vertical = Input.GetAxis(Vertical);
        
        _anim.SetFloat("Horizontal_f", horziontal);
        _anim.SetFloat("Vertical_f", vertical);
        
        Vector3 movement = new Vector3(horziontal, 0, vertical);
        
        transform.Translate(movement * moveSpeed * Time.deltaTime,Space.World);
        //_rb.velocity = new Vector3(horziontal, 0, vertical) * moveSpeed;
    }

    private void Aim()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
    }

    public delegate void ShootingDelegate();
    public static event ShootingDelegate shootPressed;

    
    private void Shoot()
    {
    

        // if (_playerWeapons.WeaponId == AR)
        {
            if (Input.GetMouseButton(0))
            {
                // bool true for rifle continues shooting
                shootPressed?.Invoke();
            }
        }
        

        if (Input.GetMouseButtonUp(0))
        {
            // bool false for rifle continues shooting (stop shooting)

        }
    }
    
}
