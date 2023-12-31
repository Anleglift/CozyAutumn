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
    public CharacterMovement CharacterMovement;
    private void Start()
    {
        startTime = Time.time;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (!IsHeld)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (CharacterMovement.moveTowardsCursor == false)
            {
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
                    animator.SetBool("Stand_Up", false);
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
            }else
            {
                float distance2 = Vector3.Distance(transform.position, CharacterMovement.worldPosition);
                if (distance2 <= 1f)
                {
                    Debug.Log("close");
                    animator.SetBool("Run", false);
                }
                else
                {
                    animator.SetBool("Stand_Up", true);
                    animator.SetBool("Sit", false);
                    animator.SetBool("Run", true);
                }
            }
        }
        else
        {
         
        }
    }
}
