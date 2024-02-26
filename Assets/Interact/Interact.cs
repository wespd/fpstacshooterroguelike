using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public float interactDistance;
    public Image canInteractImage;
    public Image cantInteractImage;
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit);
        Interactable hitObject = hit.collider != null? hit.collider.GetComponent<Interactable>() : null;
        bool canInteract = Vector3.Distance(hit.point, transform.position) <= interactDistance && hitObject != null;
        if(canInteract)
        {
            canInteractImage.enabled = true;
            cantInteractImage.enabled = false;
        }
        else
        {
            cantInteractImage.enabled = true;
            canInteractImage.enabled = false;
        }
        if(Input.GetMouseButtonDown(0) && canInteract)
        {   
            hitObject.InteractTrigger(this);
        }
        
    }
}
