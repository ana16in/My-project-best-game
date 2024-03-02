using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity = 9.8f;
    public float jumpForce;
    public float speed;
    public Animator Animator;

    private  float _fallVelosity = 0;
    private Vector3 _moveVector;

    private CharacterController _characterController;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movement
        _moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _moveVector += transform.forward;    
        }

        if (Input.GetKey(KeyCode.S))
        {
            _moveVector -= transform.forward;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _moveVector += transform.right;         
        }

        if (Input.GetKey(KeyCode.A))
        {
            _moveVector -= transform.right; 
        } 
        
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded)
        {
            _fallVelosity = -jumpForce;
            Animator.SetBool("isGrounded", false);
        }
        
        if (_moveVector == Vector3.zero)
        {
            Animator.SetBool("isRunning", false);
        }
        else 
        {
            Animator.SetBool("isRunning", true);
        }
    }
   
    void FixedUpdate()
    {
        // Movement
        _characterController.Move(_moveVector * speed * Time.fixedDeltaTime);

        // Fall and jump
        _fallVelosity += gravity * Time.fixedDeltaTime;
        _characterController.Move(Vector3.down * _fallVelosity * Time.fixedDeltaTime);

        // Stop fall if on the ground
        if(_characterController.isGrounded)
        {
            _fallVelosity = 0;
            Animator.SetBool("isGrounded", true);
        }
    }
}
