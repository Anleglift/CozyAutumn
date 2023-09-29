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
    public float fallSpeed = 10.0f;
    public float walkfallspeed = 11.5f;// Adjust the fall speed as needed
    public GameObject GroundCheck;
    public bool isGrounded; // Flag to store whether the player is on the ground
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(GroundCheck.transform.position, Vector3.down, groundRayDistance);

        // If not grounded, move the player downward

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
            if (!isGrounded)
            {
                Vector3 fallVector = Vector3.down * walkfallspeed * Time.deltaTime;
                transform.position += fallVector;
            }
            animator.SetBool("Run", true);
        }
        else
        {
            if (!isGrounded)
            {
                Vector3 fallVector = Vector3.down * fallSpeed * Time.deltaTime;
                transform.position += fallVector;
            }
            animator.SetBool("Run", false);
        }
    }
}