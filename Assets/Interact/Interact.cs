using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public float interactDistance;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit);
            if(hit.collider != null && hit.distance < interactDistance)
            {
                Interactable hitObject = hit.collider.GetComponent<Interactable>();
                if(hitObject != null && hit.distance >= interactDistance)
                {
                    hitObject.InteractTrigger(this);
                }
            }
        }
        
    }
}
