using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUpdate : MonoBehaviour
{
    // Update is called once per frame
    public void adjustMouse(float newVal)
    {
        MainMenuScript.mouseSensitivity = newVal;
    }
    public void adjustMove(float newVal)
    {
        MainMenuScript.movementSpeed = newVal*50;
    }
}
