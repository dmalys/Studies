using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRedScript : MonoBehaviour {
    // Use this for initialization

    void Start () {
       // MainMenuScript menuScript = GetComponent<MainMenuScript>();
        
        Debug.Log("start doorred");
        if (MainMenuScript.door != 0)
        {
            Debug.Log("door: "+MainMenuScript.door);
            GameObject doorToChange = GameObject.Find("door " + MainMenuScript.door);
           // GameObject doorToChange = GameObject.Find("Component#8 9");
            doorToChange.GetComponentInChildren<Renderer>().material.color= Color.red;
            
                doorToChange.GetComponentInChildren<SphereCollider>().enabled = true;
            
        }
    }
}
