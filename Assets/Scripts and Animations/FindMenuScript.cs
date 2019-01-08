using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FindMenuScript : MonoBehaviour
{
    public GameObject camer2;

    public void ContinueMenu()
    {
        camer2.GetComponent<Camera_Look>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       

    }

   public void ExitMenu()
    {
        
        SceneManager.LoadScene(sceneBuildIndex: 0);
        
    }
   
}
