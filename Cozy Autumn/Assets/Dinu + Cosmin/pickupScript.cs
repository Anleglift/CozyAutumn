using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class pickupScript : MonoBehaviour
{
    [Header("setari")]
    public float delayTime;
    public float throwForce;
    public float throwHeight;


    [Header("alelalyte")]
    public GameObject fox;
    public GameObject character;
    public NavMeshAgent navObstacle;
    public Transform handPos;
    public GameObject childObj;
    public CapsuleCollider colider;
<<<<<<< Updated upstream

    void getFox()
    {
        Transform getHand = character.transform.Find("RightHand");
        Debug.Log(getHand.position);
    }
=======
    
    private Rigidbody rb;
    private bool isHeld = false;

    [Header("MUIEEE BAA")]
    private bool pula = false;
>>>>>>> Stashed changes
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHeld && pula == true)
            {
                isHeld = true;
                fox.transform.position = character.transform.Find("RightHand").position;
                navObstacle.enabled = false;
                childObj.transform.SetParent(handPos);
<<<<<<< Updated upstream
                colider.enabled = false;
=======
                rb.isKinematic = true;
>>>>>>> Stashed changes
            }
            else
            {
                isHeld = false;
                navObstacle.enabled = true;
                childObj.transform.SetParent(null);
<<<<<<< Updated upstream
=======
                rb.isKinematic = false;
>>>>>>> Stashed changes
            }

            
        }

<<<<<<< Updated upstream


=======
        if (isHeld && Input.GetMouseButtonDown(0))
        {
            isHeld = false;
            navObstacle.enabled = true;
            childObj.transform.SetParent(null);
            rb.isKinematic = false;
            Vector3 throwDirection = character.transform.forward + Vector3.up * throwHeight;
            rb.velocity = throwDirection * throwForce;
            Invoke("Delay", delayTime);
        }
>>>>>>> Stashed changes
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "fox")
            pula = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "fox")
            pula = false;
    }
}
