using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

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

    [SerializeField] private float turnSpeed;
    
    [SerializeField] private GameObject mouseObject;
    [SerializeField] private Rig mainIKRig;
    
    
    #region Private Refrences

    private Rigidbody _rb;
    private Animator _anim;


    private bool _shiftHold;
    #endregion

    public bool isDancing;

    #region Firing

    private RaycastWeapon weapon;
    

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        _anim = GetComponent<Animator>();
        isDancing = false;
        weapon = GetComponentInChildren<RaycastWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        // Shoot();
        LookAtObject();
        ClappingMechanic();
    }

    private void LateUpdate()
    {
  
        
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
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.down);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
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
                mouseObject.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
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
            mainIKRig.weight = 0.0f;
        }
        else
        {
            _anim.SetBool("Clapping", false);

            isDancing = false;
            mainIKRig.weight = 1.0f;
        }
     
    }
}
