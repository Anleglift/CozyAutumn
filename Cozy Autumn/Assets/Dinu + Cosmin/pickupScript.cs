using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pickupScript : MonoBehaviour
{
    public Transform throwDestination;
    public float throwHeight = 5f; // Adjust this for the desired arc height
    private GameObject carriedObject;
    public NavMeshAgent agent;
    public Transform Hand;
    public Transform FoxPlace;
    public FoxFollow FoxFollow;
    private Vector3 initialCarryPosition;
    private Vector3 throwStartPosition;
    private float throwStartTime;
    public float throwDuration = 2.0f; // Adjust this for the desired throw duration
    public bool CanThrow = true;
    public Transform ThrowPosition;
    public GameObject Fox;
    public GameObject Fox2;
    public Animator animator;
    bool isGrounded = true;
    public ThirdPersonMovement third;
    public Transform CheckGround;
    public float groundRayDistance = 1.0f; // Adjust this value based on your character's size
    public GameObject Rotation;
    public GameObject Object;
    public GameObject Player;
    void Update()
    {
        if (CanThrow)
        {
            // Check for 'E' key press to pick up or throw
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (third.direction.magnitude < 0.1f)
                {
                    if (carriedObject == null)
                    {
                        Fox2.transform.localRotation = Quaternion.Euler(0f,Player.transform.rotation.y,0f);
                        Fox2.transform.localPosition = new Vector3(FoxPlace.transform.position.x,FoxPlace.transform.position.y,FoxPlace.transform.position.z+0.5f);
                        Fox.transform.localPosition = new Vector3(0f, -1.369999f, 0f);
                        Fox.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                        FoxFollow.IsHeld = true;
                        animator.SetBool("Off", false);
                        animator.SetBool("Sit", false);
                        animator.SetBool("Stand_Up", true);
                        animator.SetBool("Jump", true);
                        // Attempt to pick up an object within reach
                        TryPickupObject();
                    }
                    else
                    {
                        LetDownObject();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (carriedObject != null)
                {
                    animator.SetBool("Landed", false);
                    animator.SetBool("Throw", true);
                    animator.SetBool("Jump", false);
                    ThrowCarriedObject();
                }
            }
        }
    }
    void ResetPosition()
    {
        Fox.transform.localPosition = new Vector3(0f, -1.369999f, 0f);
        Fox.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }
    private void LetDownObject()
    {
        animator.SetBool("Off", true);
        agent.enabled = true;
        carriedObject.transform.SetParent(null);
        carriedObject = null;
        FoxFollow.IsHeld = false;
        animator.SetBool("Jump", false);
        Invoke("ResetPosition",1);
    }
    void TryPickupObject()
    {
        Fox.transform.localPosition = new Vector3(0f, -1.369999f, 0f);
        Fox.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, FoxPlace.position - transform.position, out hit, 5f))
        {
            // Check if the hit object has a NavMeshAgent
            NavMeshAgent hitAgent = hit.collider.GetComponent<NavMeshAgent>();
            Debug.Log(hit.transform.position);
            if (hitAgent != null)
            {
                carriedObject = hit.collider.gameObject;
                agent.enabled = false;
                carriedObject.transform.SetParent(Hand);
                carriedObject.transform.position = Hand.transform.position;
                initialCarryPosition = carriedObject.transform.position;
            }
        }
    }

    void ThrowCarriedObject()
    {
        Fox.transform.localPosition = new Vector3(0f, -1.369999f, 0f);
        Fox.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        carriedObject.transform.position = ThrowPosition.position;
        if (carriedObject != null)
        {
            agent.enabled = true;
            carriedObject.transform.SetParent(null);
            throwStartTime = Time.time;
            throwStartPosition = carriedObject.transform.position;
            // Calculate the throw direction
            Vector3 throwDirection = (throwDestination.position - transform.position).normalized;
            StartCoroutine(PerformThrow(throwDirection));
        }
    }

    IEnumerator PerformThrow(Vector3 throwDirection)
    {
        CanThrow = false;
        float elapsedTime = 0f;
        while (elapsedTime < throwDuration)
        {
            float normalizedTime = elapsedTime / throwDuration;
            float height = Mathf.Sin(normalizedTime * Mathf.PI) * throwHeight;

            Vector3 newPosition = Vector3.Lerp(throwStartPosition, throwStartPosition + (throwDirection * 10f), normalizedTime);
            newPosition.y += height;
            carriedObject.transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Fox.transform.localPosition = new Vector3(0f, -1.369999f, 0f);
        Fox.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        FoxFollow.IsHeld = false;
        carriedObject = null; // Object is no longer carried
        CanThrow = true;
        animator.SetBool("Throw", false);
        animator.SetBool("Landed", true);
    }
}
