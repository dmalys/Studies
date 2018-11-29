using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {
    public bool auto = false;
    public int i = 1;
    public int door=0;
    public InputField inputDoor;
    public Text ftext;
    public void PLayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + i);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Toogle_Changed()
    {
        
        if (i == 2)
        {
            i = 1;
        }
        else if (i == 1)
        {
            i = 2;
        }
    }
    public void ChooseDoor()
    {
        door = 0;
        door = int.Parse(inputDoor.text);
        ftext.text = door.ToString();
    }
    
}
