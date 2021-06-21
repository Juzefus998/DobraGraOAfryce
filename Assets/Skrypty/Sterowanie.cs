using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sterowanie : MonoBehaviour
{
    
    public float _moveSpeed = 5f;
    
    public float _gravity = 9.81f;
    
    public float _jumpSpeed = 3.5f;
    
    public float _doubleJumpMultiplier = 0.5f;
	public Transform cam;
	public float speed = 6f;
	public float turnSmoothTime = 0.1f;
	float turnSmoothVelocity;

    public CharacterController _controller;

    private float _directionY;

    private bool _canDoubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
     
	    float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        
		if (direction.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
			
			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			
			
		
		}
		
		if (Input.GetKey("left shift") && _controller.isGrounded)
        {
            speed = 12f;
        }
        else
        {
            speed = 6f;
        }
		if (_controller.isGrounded)
        {
            _canDoubleJump = true;

            if (Input.GetButtonDown("Jump"))
            {
                _directionY = _jumpSpeed;
            }
        } else {
            if(Input.GetButtonDown("Jump") && _canDoubleJump) {
                _directionY = _jumpSpeed * _doubleJumpMultiplier;
                _canDoubleJump = false;
            }
        }

        _directionY -= _gravity * Time.deltaTime;

        direction.y = _directionY;

        _controller.Move(direction * _moveSpeed * Time.deltaTime);
    }
}