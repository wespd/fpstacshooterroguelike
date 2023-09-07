using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public float sensitivity;
    public Camera cam;
    public GameObject camObj; 
    float mousePositionX;
    float mousePositionY;
    public Vector2 yLookRange;
    void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        mousePositionX += Input.GetAxis("Mouse X") * sensitivity;
        mousePositionY += Input.GetAxis ("Mouse Y") * sensitivity;

        mousePositionY = Mathf.Clamp(mousePositionY, yLookRange.x, yLookRange.y); 
        
        camObj.transform.localRotation = Quaternion.Euler(-mousePositionY, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, mousePositionX, 0f);
    }
}
