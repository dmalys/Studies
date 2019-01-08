using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera_Move: MonoBehaviour
{
    

    private CharacterController charController;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        
        PlayerMovement();
    }

    private void PlayerMovement()
    {

        float verticalInput = Input.GetAxis("Vertical") * MainMenuScript.movementSpeed;
        float horizonztalInput = Input.GetAxis("Horizontal") * MainMenuScript.movementSpeed;

        Vector3 forwardMovement = transform.forward * verticalInput;
        Vector3 rightMovement = transform.right * horizonztalInput;

        charController.SimpleMove(forwardMovement + rightMovement);
        ExitInput();
    }

    public void ExitInput()
    {
       // if (Input.GetKeyDown(KeyCode.Escape))
      //  {
      //      SceneManager.LoadScene(0);
      //      Cursor.visible = true;
      //      Cursor.lockState = CursorLockMode.None;
      //  }
    }
}