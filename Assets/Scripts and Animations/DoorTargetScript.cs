using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorTargetScript : MonoBehaviour
{
    [SerializeField]
   private GameObject menu;
    public GameObject camer;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {

            camer = GameObject.Find("Main Camera");
            camer.GetComponent<Camera_Look>().enabled = false;
            Cursor.lockState =CursorLockMode.None;
            Cursor.visible = true;
            menu.SetActive(true);
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                GameObject aAlgorithm = GameObject.Find("A*");
                aAlgorithm.GetComponent<AMovement>().enabled = false;
                GameObject aAlgorithm1 = GameObject.Find("A*1");
                aAlgorithm1.GetComponent<AMoveFloor>().enabled = false;
                GameObject aAlgorithm2 = GameObject.Find("A*2");
                aAlgorithm2.GetComponent<AMoveFloor>().enabled = false;
                GameObject aAlgorithm3 = GameObject.Find("A*3");
                aAlgorithm3.GetComponent<AMoveFloor>().enabled = false;

            }
            
        }
    }
}
