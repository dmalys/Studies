using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRedScript : MonoBehaviour {
    // Use this for initialization
    int doorNum = 0;

    void Start () {
       // MainMenuScript menuScript = GetComponent<MainMenuScript>();
        doorNum = 2;//menuScript.door;
        Debug.Log("start doorred");
        if (MainMenuScript.door != 0)
        {
            Debug.Log("if door");
            Debug.Log("door: "+MainMenuScript.door);
            GameObject doorToChange = GameObject.Find("door " + MainMenuScript.door);
           // GameObject doorToChange = GameObject.Find("Component#8 9");
            doorToChange.GetComponentInChildren<Renderer>().material.color= Color.red;

        }
    }
	
	// Update is called once per frame
	void Update () {

        

    }
}
