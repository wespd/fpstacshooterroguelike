using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public float sensitivity;
    public Camera cam;
    
    float xRotation;
    float yRotation;

    public Transform orientation;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        xRotation += Input.GetAxis("Mouse X") * sensitivity;
        yRotation += Input.GetAxis("Mouse Y") * sensitivity;

        yRotation = Mathf.Clamp(yRotation, -90, 90);

        transform.rotation = Quaternion.Euler(-yRotation, xRotation, 0);
        orientation.rotation = Quaternion.Euler(0, xRotation, 0);
    }
}
