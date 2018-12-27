using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTargetScript : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            GameObject aAlgorithm = GameObject.Find("A*");
            aAlgorithm.GetComponent<AMovement>().enabled=false;
        }
    }
}
