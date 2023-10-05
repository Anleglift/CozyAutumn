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
    private float throwDuration = 1.0f; // Adjust this for the desired throw duration
    public bool CanThrow = true;
    void Update()
    {
        if (CanThrow)
        {
            // Check for 'E' key press to pick up or throw
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (carriedObject == null)
                {

                    // Attempt to pick up an object within reach
                    TryPickupObject();
                }
                else
                {
                    // Throw the carried object
                    ThrowCarriedObject();
                }
            }
        }
    }

    void TryPickupObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, FoxPlace.position - transform.position, out hit, 5f))
        {
            // Check if the hit object has a NavMeshAgent
            NavMeshAgent hitAgent = hit.collider.GetComponent<NavMeshAgent>();
            Debug.Log(hit.transform.position);
            if (hitAgent != null)
            {

                FoxFollow.IsHeld = true;
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
        FoxFollow.IsHeld = false;
        carriedObject = null; // Object is no longer carried
        CanThrow = true;
    }
}
