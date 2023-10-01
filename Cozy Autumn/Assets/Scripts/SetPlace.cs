using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    private bool moveTowardsCursor = false;
    private bool FoundPosition = false;
    public Vector3 worldPosition;
    public NavMeshAgent navMeshAgent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Change this to the desired key or button
        {
            if (moveTowardsCursor == true)
                moveTowardsCursor = false;
            else
            {
                moveTowardsCursor = true;
                MoveTowardsCursor();
            }
        }

        if (FoundPosition == true)
        {
            navMeshAgent.SetDestination(worldPosition);
            if (Vector3.Distance(transform.position, worldPosition) < 0.1f)
                FoundPosition = false;
        }
    }

    private void MoveTowardsCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Assign the cursor position to the class-level worldPosition variable
            worldPosition = hit.point;

            FoundPosition = true;
        }
    }
}