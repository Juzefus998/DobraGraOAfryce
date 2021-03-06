using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkryptNaFPS : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] float JumpHeight = 200;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] Animator anim;
    

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    void Idle()
    {
        anim.SetFloat("Speed", 0);
    }
    void Walk()
    {
        anim.SetFloat("Speed", 0.1f);
    }
     void Run()
    {
        anim.SetFloat("Speed", 0.5f);
    }
     void Sprint()
    {
        anim.SetFloat("Speed", 1);
    }
    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
        //if (!(!Input.GetKeyDown("up") && !Input.GetKeyDown("down") && !Input.GetKeyDown("left") && !Input.GetKeyDown("right") && !Input.GetKeyDown("left shift")) != false)
        //{
        //    return;    //KIEDYŚ TO BYŁO!
        //}
        //Idle();
        if (Input.anyKey == false)
        {
            Idle();
        }

    }


    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
            velocityY = 0.0f;

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey("left shift"))
        {
            walkSpeed = 12f;
            Sprint();   
        }

        else
        {
            walkSpeed = 6f;
            Run();
        }

         
      }

    }

