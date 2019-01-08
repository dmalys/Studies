using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Look : MonoBehaviour
{

    [SerializeField]
    private Transform playerBody;

    private float blockVal;

    private void Awake()
    {
        LockCursor();
        blockVal = 0.0f;
    }
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * MainMenuScript.mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MainMenuScript.mouseSensitivity * Time.deltaTime;

        blockVal += mouseY;

        if (blockVal > 90.0f)
        {
            blockVal = 90.0f;
            mouseY = 0.0f;
            ResetAxisToValue(270.0f);
        }
        else if (blockVal < -90.0f)
        {
            blockVal = -90.0f;
            mouseY = 0.0f;
            ResetAxisToValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ResetAxisToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}