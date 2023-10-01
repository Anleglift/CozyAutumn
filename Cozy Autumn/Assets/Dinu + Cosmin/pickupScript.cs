using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pickupScript : MonoBehaviour
{
    public GameObject fox;
    public GameObject character;
    private bool isHeld = false;
    public NavMeshAgent navObstacle;
    public Transform handPos;
    public GameObject childObj;
    public CapsuleCollider colider;
    public float throwForce;
    public float throwHeight;
    private Rigidbody rb;
    public float delayTime;

    void Start()
    {
        rb = fox.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Delay()
    {
        rb.isKinematic = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHeld)
            {
                isHeld = true;
                fox.transform.position = character.transform.Find("RightHand").position;
                navObstacle.enabled = false;
                childObj.transform.SetParent(handPos);
                colider.enabled = false;
                rb.isKinematic = true;
            }
            else
            {
                isHeld = false;
                navObstacle.enabled = true;
                childObj.transform.SetParent(null);
                colider.enabled = true;
                rb.isKinematic = false;
            }
        }

        if (isHeld && Input.GetMouseButtonDown(0))
        {
            isHeld = false;
            navObstacle.enabled = true;
            childObj.transform.SetParent(null);
            colider.enabled = true;
            rb.isKinematic = false;
            Vector3 throwDirection = character.transform.forward + Vector3.up * throwHeight;
            rb.velocity = throwDirection * throwForce;
            Invoke("Delay", delayTime);
        }
    }
}
