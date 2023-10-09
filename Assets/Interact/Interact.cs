using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit);
            if(hit.collider != null)
            {
                Interactable hitObject = hit.collider.GetComponent<Interactable>();
                if(hitObject != null)
                {
                    hitObject.InteractTrigger(this);
                }
            }
        }
        
    }
}
