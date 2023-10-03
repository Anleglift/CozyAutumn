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

    void getFox()
    {
        Transform getHand = character.transform.Find("RightHand");
        Debug.Log(getHand.position);
    }
    void Start()
    {

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
            }
            else
            {
                isHeld = false;
                navObstacle.enabled = true;
                childObj.transform.SetParent(null);
            }

            
        }



    }
}
