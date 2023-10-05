using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FoxFollow : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;
    private bool moveTowardsCursor = false;
    public Animator animator;
    public ThirdPersonMovement ThirdPersonMovement;
    public float timeThreshold = 5.0f; // Adjust this to the desired threshold in seconds
    private float startTime;
    public bool sit = false;
    public bool satdown = false;
    public bool IsHeld = false;
    private void Start()
    {
        startTime = Time.time;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (!IsHeld)

        {
            if (Input.GetKeyDown(KeyCode.T)) // Change this to the desired key or button
        {
            if (moveTowardsCursor == true)
                moveTowardsCursor = false;
            else
                moveTowardsCursor = true;
        }
        if (player != null && moveTowardsCursor == false)
            float distance = Vector3.Distance(transform.position, player.position);
            if (player != null)
            {
                // Set the destination of the NavMeshAgent to the player's position
                navMeshAgent.SetDestination(player.position);
            }
            if (ThirdPersonMovement.direction.magnitude > 0.1f)
            {
                sit = false;
                startTime = Time.time; // Reset the timer if the boolean becomes true
                if (satdown)
                {
                    animator.SetBool("Stand_Up", true);
                    animator.SetBool("Run", true);
                    satdown = false;
                    animator.SetBool("Sit", false);
                }
                else
                {
                    animator.SetBool("Run", true);
                }

            }
            else
            {
                animator.SetBool("Run", true);
                if (distance <= 0.5f)
                {
                    animator.SetBool("Stand_Up", false);
                    Debug.Log("In Position");
                    animator.SetBool("Run", false);
                    sit = true;
                    if (sit)
                    {
                        float elapsedTime = Time.time - startTime;
                        Debug.Log(elapsedTime);
                        if (elapsedTime >= timeThreshold)
                        {
                            animator.SetBool("Sit", true);
                            satdown = true;
                        }
                    }
                }

            }
        }
    }
}
