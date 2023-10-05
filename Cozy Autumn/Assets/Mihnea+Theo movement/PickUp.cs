using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject FoxOnPlayer;
    public GameObject FoxOnGround;
    public GameObject Player;
    public GameObject Fox;
    public float waitTime = 1.0f;
    public float timer = 0f;
    public bool PickedUp = false;
    public bool Ok = false;
    public bool interact = false;
    void Start()
    {
        FoxOnPlayer.SetActive(false);
        FoxOnGround.SetActive(true);
    }
    public void OnTriggerStay(Collider other)
    {
        interact = true;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            timer = timer - waitTime;
            Ok = true;
        }
        if (Input.GetKey(KeyCode.E)&& Ok == true)
        {
            Ok = false;
            if (PickedUp == false &&  interact == true )
            {
                FoxOnGround.SetActive(false);
                FoxOnPlayer.SetActive(true);
                PickedUp = true;
            }
            else
                if(PickedUp == true)
                {
                    Fox.SetActive(false);
                    Fox.transform.position = Player.transform.position + new Vector3(0.5f, 0f, 0.5f);
                    Fox.SetActive(true);
                    FoxOnGround.SetActive(true);
                    FoxOnPlayer.SetActive(false);
                    PickedUp = false;
                }
        }
        interact = false;
    }

}