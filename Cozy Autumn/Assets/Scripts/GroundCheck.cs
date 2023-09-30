using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float groundRayDistance = 1.0f; // Adjust this value based on your character's size
    public float fallSpeed = 10.0f; // Adjust the fall speed as needed
    public GameObject Player;
    public bool isGrounded; // Flag to store whether the player is on the ground

    void Update()
    {
        // Perform a downward raycast to check if the player is on the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundRayDistance);

        // If not grounded, move the player downward
        if (!isGrounded)
        {
            Vector3 fallVector = Vector3.down * fallSpeed * Time.deltaTime;
            Player.transform.position += fallVector;
        }
    }
}
