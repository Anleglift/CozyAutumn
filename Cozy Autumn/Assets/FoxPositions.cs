using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxPositions : MonoBehaviour
{
    public GameObject Walk;
    private RaycastHit hit;
    private bool isGrounded;
    public float groundRayDistance = 1.0f; // Adjust this value based on your character's size

    // Update is called once per frame
    void Update()
    {
        // Cast a ray downward to check if the object is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundRayDistance);

        // If the object is grounded, set its Y position to the ground position
        if (isGrounded)
        {
            Vector3 newPosition = new Vector3(Walk.transform.position.x, hit.point.y, Walk.transform.position.z);
            transform.position = newPosition;
        }
        else
        {
            // If not grounded, keep the Y position as is (e.g., in mid-air)
            transform.position = new Vector3(Walk.transform.position.x, transform.position.y, Walk.transform.position.z);
        }
    }
}
