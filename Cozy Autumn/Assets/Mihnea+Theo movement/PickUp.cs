using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject FoxOnPlayer;
    public GameObject FoxOnGround; 
    public float waitTime = 2.0f;
    public float timer = 0.0f;
    public bool PickedUp = false;
    public bool Ok = false;
    void Start()
    {
        FoxOnPlayer.SetActive(false);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            timer = timer - waitTime;
            Ok = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E)&& Ok == true)
        {
            Ok = false;
            if (PickedUp == false)
            {
                FoxOnGround.gameObject.SetActive(false);
                FoxOnPlayer.SetActive(true);
                PickedUp = true;
            }
            else
            {
                FoxOnGround.gameObject.SetActive(true);
                FoxOnPlayer.SetActive(false);
                PickedUp = false;
            }
        }
    }

}