using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform playerBody;

    Transform cameraTransform;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        transform.rotation = playerBody.rotation;
        //transform.position = playerBody.transform.position;
        // transform.position += cameraTransform.forward * (Time.deltaTime);
        //transform.position = cameraTransform.forward * (Time.deltaTime );
       // Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 80, 2 * Time.deltaTime);
    }
}
