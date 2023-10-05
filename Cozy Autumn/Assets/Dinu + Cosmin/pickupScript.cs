using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pickupScript : MonoBehaviour
{
    [Header("setari")]
    public float throwForce;
    public float delayTime = 1f;


    [Header("celelalte")]
    public GameObject fox;
    public GameObject character;
    public NavMeshAgent navObstacle;
    public Transform handPos;
    public GameObject childObj;
    public CapsuleCollider colider;
    public float throwForce;
    public float throwHeight;
    private Rigidbody rb;
    public float delayTime;
    private bool isHeld = false;
    private bool isHeldCol = false;
    private Rigidbody rb;
    void Start()
    {
        rb = fox.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHeld && isHeldCol == true)
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
            Vector3 throwDirection = character.transform.forward;
            rb.velocity = throwDirection * throwForce;
            Invoke("Delay", delayTime);
        }
    }
    void Delay()
    {
        rb.isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "fox")
        {
            isHeldCol = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "fox")
        {
            isHeldCol = false;
        }
    }

}
