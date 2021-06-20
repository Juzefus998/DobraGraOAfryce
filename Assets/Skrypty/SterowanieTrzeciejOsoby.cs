﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class SterowanieTrzeciejOsoby : MonoBehaviour
{
    public CharacterController controller;
	public Transform cam;
	public Rigidbody rb;
	
	public float speed = 6f;
	public float gravity = -9.81f;
	
	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;
	
	public float turnSmoothTime = 0.1f;
	float turnSmoothVelocity;
	
	Vector3 velocity;
	bool isGrounded;
	
	
    // Update is called once per frame
    void Update()
    {
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		
		if(isGrounded && velocity.y < 0)
		{
		
		velocity.y = -2f;
		}
	    if (Input.GetKey("left shift") && isGrounded)
        {
            speed = 48f;
        }
        else
        {
            speed = 12f;
        }
		if (Input.GetKey("space") && isGrounded) 
		{
			rb.AddForce(new Vector3(0,1,0),ForceMode.Impulse);
		}
			
		
        float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
		
		if (direction.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
			
			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			
			controller.Move(moveDir.normalized * speed * Time.deltaTime);
			velocity.y += gravity * Time.deltaTime;
			
			controller.Move(velocity * Time.deltaTime);
		}
		
	}
	
}
		
	
