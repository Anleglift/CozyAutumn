using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFox : MonoBehaviour
{
    public GameObject Fox;
    public GameObject Foy;
    public void ResetAndSpawnFox()
    {
        Fox.transform.rotation = Quaternion.Euler(0,Foy.transform.rotation.y,0);
    }
}
