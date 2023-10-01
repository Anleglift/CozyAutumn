using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoxFollow : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;
    private bool moveTowardsCursor = false;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Change this to the desired key or button
        {
            if (moveTowardsCursor == true)
                moveTowardsCursor = false;
            else
                moveTowardsCursor = true;
        }
        if (player != null && moveTowardsCursor == false)
        {
            // Set the destination of the NavMeshAgent to the player's position
            navMeshAgent.SetDestination(player.position);
        }
    }
}
