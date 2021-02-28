using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const int Pistol = 1;
    private const int Shotgun = 2;
    private const int AR = 3;
    
    private const string Horizontal = "Horizontal";

    private const string Vertical = "Vertical";

    private float horizontal;
    private float vertical;
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
        horizontal = Input.GetAxis(Horizontal);
        vertical = Input.GetAxis(Vertical);
        AnimatingMovement(horizontal,vertical);
 
        
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        
        transform.Translate(movement * moveSpeed * Time.deltaTime,Space.World);
        //_rb.velocity = new Vector3(horizontal, 0, vertical) * moveSpeed;
    }

    private Vector3 moveDirection = Vector3.zero;

    void AnimatingMovement(float h, float v)
    {
        moveDirection = new Vector3(h, 0, v);
        if (moveDirection.magnitude > 1.0f)
        {
            moveDirection = moveDirection.normalized;
        }

        moveDirection = transform.InverseTransformDirection(moveDirection);
        _anim.SetFloat("Horizontal_f", moveDirection.x,0.05f,Time.deltaTime);
        _anim.SetFloat("Vertical_f", moveDirection.z, 0.05f,Time.deltaTime);
    }

    private void Aim()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
    }

    public delegate void ShootingDelegate();
    public static event ShootingDelegate shootPressed;
    public static event ShootingDelegate shotgunShot;

    
    private void Shoot()
    {
        
        {
            if (Input.GetMouseButton(0))
            {
                // bool true for rifle continues shooting
                shootPressed?.Invoke();
                if (_playerWeapons.WeaponId == Shotgun)
                {
                    shotgunShot?.Invoke();
                }
            }
        }
        

        if (Input.GetMouseButtonUp(0))
        {
            // bool false for rifle continues shooting (stop shooting)

        }
    }
    
}
