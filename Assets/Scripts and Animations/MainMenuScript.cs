using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {
    public bool auto = false;
    public int i = 1;
    public static int door=0;
    public InputField inputDoor;
    public Text ftext;
    public static bool ifCorrect = false;
    public static bool ifCorrectPassing = false;
    public List<int> intList = new List<int> {3,4,5,6,7,10,11,12,15,16,17,18,19,20,21,102,103,104,105,106,107,108,110,111,112,113,115,116,117,118,119,202,203,204,205,207,208,209,211,212,213,214,215,216,217,218,220,221,222,223,224,225,226,302,303,304,305,306,307,308,309,311,312,313,314,315,316,317,318,320,321,322,323,324,325};
    public GameObject DoorChooser;
    public GameObject WrongDoorPrompt;


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
        
        Debug.Log("bool: " + ifCorrect.ToString());
        ifCorrect = (intList.IndexOf(door) != -1);
        switch (door)
        {
            case 16:
                door = 15;
                break;
            case 18:
                door = 17;
                break;
            case 21:
                door = 20;
                break;
            case 116:
                door = 115;
                break;
            case 118:
                door = 117;
                break;
            case 121:
                door = 120;
                break;
            case 107:
                door = 105;
                break;
            case 106:
                door = 105;
                break;
            case 223:
                door = 222;
                break;
            case 221:
                door = 220;
                break;
            case 321:
                door = 320;
                break;
            case 323:
                door = 322;
                break;
        }
    }
    public void PLayGame()
    {
        Debug.Log("bool2: " + ifCorrect.ToString());
        if (ifCorrect)
        {
            Debug.Log(i);
            SceneManager.LoadScene(sceneBuildIndex: i);
            Cursor.visible = false;
        }
        else
        {
            DoorChooser.SetActive(false);
            WrongDoorPrompt.SetActive(true);
        }
    }

}
