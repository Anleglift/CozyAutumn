using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Animator animator;
    public float groundRayDistance = 1.0f; // Adjust this value based on your character's size
    public Vector3 direction;
    // Gravity variables
    public float gravity = -9.81f;
    Vector3 velocity;
    bool isGrounded;
    public float fallspeed=10f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundRayDistance);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset the vertical velocity when grounded
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime * fallspeed * 10;
            controller.Move(velocity * Time.deltaTime);
            animator.SetBool("Run", true);
        }
        else
        {
            velocity.y += gravity * Time.deltaTime * fallspeed;
            controller.Move(velocity * Time.deltaTime);
            animator.SetBool("Run", false);
        }

        // Apply gravity
        
    }
}
