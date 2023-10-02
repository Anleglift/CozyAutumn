using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject FoxOnPlayer;
    void Start()
    {
        FoxOnPlayer.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
               this.gameObject.SetActive(false);
                FoxOnPlayer.SetActive(true);
            }
        }
    }

}