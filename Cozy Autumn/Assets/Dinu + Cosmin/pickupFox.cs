using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    private Rigidbody rb;
    private bool isHeld = false;
    

    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) &&  isHeld == false)
        {
            isHeld = true;
            rb.isKinematic = true;
            rb.useGravity = false;
        }
        
        if(isHeld)
        {

        }

        if (Input.GetKey(KeyCode.E) && isHeld == true)
        {
            isHeld = false;
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}
