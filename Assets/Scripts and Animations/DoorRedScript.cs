using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRedScript : MonoBehaviour {
    

    void Start () {
       
        
        Debug.Log("start doorred");
        if (MainMenuScript.door != 0)
        {
            Debug.Log("door: "+MainMenuScript.door);
            GameObject doorToChange = GameObject.Find("door " + MainMenuScript.door);
          
            doorToChange.GetComponentInChildren<Renderer>().material.color= Color.red;
            
                doorToChange.GetComponentInChildren<SphereCollider>().enabled = true;
            
        }
    }
}
