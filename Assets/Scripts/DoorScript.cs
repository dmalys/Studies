using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    Animator anim;
    bool flag;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        flag = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player"){ 
            if (flag)
                anim.SetTrigger("OpenDoor");
            anim.enabled = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            anim.enabled = true;
        }
    }
    void pauseAnimationEvent()
    {
        anim.enabled = false;
    }
}
