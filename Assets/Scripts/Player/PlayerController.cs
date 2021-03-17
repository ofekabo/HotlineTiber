﻿using System;
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

    [SerializeField] private GameObject mouseObject;
    
    
    
    #region Private Refrences

    private Rigidbody _rb;
    private Animator _anim;
    private PlayerWeapons _playerWeapons;

    private bool _shiftHold;
    #endregion

    public bool isDancing;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerWeapons = GetComponent<PlayerWeapons>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        _anim = GetComponent<Animator>();
        isDancing = false;
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Shoot();
        LookAtObject();
        ClappingMechanic();
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

        if (movement.magnitude > 1.0f)
        {
            movement = movement.normalized;
        }
        transform.Translate(movement * moveSpeed* Time.deltaTime,Space.World);
        
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

        Vector3 Target = (mouseObject.transform.position - transform.position).normalized;
        if (!_shiftHold || Vector3.Dot(Target,transform.forward) < 0)
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
        }

    }

    private void LookAtObject() // looking at mouse
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100,Color.red);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _shiftHold = true;
                mouseObject.transform.position = new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z);
            }
            else
            {
                _shiftHold = false;
                mouseObject.transform.position = new Vector3(hit.point.x, 2, hit.point.z);
            }
                 
            
        }
        
        
        
            


    }

    public delegate void ShootingDelegate();
    public static event ShootingDelegate shootPressed;


    private void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            shootPressed?.Invoke();
        }
    }


    private void ClappingMechanic()
    {
        if (Input.GetKey(KeyCode.E))
        {
            _anim.SetBool("Clapping", true);
            isDancing = true;
        }
        else
        {
            _anim.SetBool("Clapping", false);

            isDancing = false;
        }
     
    }
}
