using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed, gravityModifier, jumpPower, runSpeed = 12f;
    public float mouseSensitivity = 2f;
    public Transform camTrans;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public Transform firePoint;
    public GameObject bullet;
    
    private CharacterController _charController;
    private Vector3 _moveInput;

    
    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }
    
    
    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }

    void Move()
    {
        // _moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        // _moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float yStore = _moveInput.y;

        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

        _moveInput = horiMove + vertMove;
        _moveInput.Normalize();
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _moveInput *= runSpeed; // running 
        }
        else
        {
            _moveInput *= moveSpeed; // moving
        }
        
        _moveInput.y = yStore;

        _moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;
        
        if (_charController.isGrounded) // I don't underatand this, good for optimising, 15 lesso
        {
            _moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }
        
        
        bool canJump = Physics.OverlapSphere(groundCheckPoint.position, .25f, whatIsGround).Length > 0;
        
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            _moveInput.y = jumpPower;
        }

        _charController.Move(_moveInput * Time.deltaTime);

        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));
    }

}
