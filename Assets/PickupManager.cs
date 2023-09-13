using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public Item currentItem;
    public GameObject itemObject;
    public Transform objectPosition;

    public float pickupRange;

    public KeyCode pickupKey = KeyCode.E;
    public KeyCode dropKey = KeyCode.Q;

    void Update()
    {
        if(Input.GetKeyDown(pickupKey))
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupRange))
            {
                Item hitItem = hit.collider.GetComponent<Item>();
                if(hitItem != null)
                {
                    currentItem = hitItem;
                }
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            currentItem.Use();
        }
        if(Input.GetKeyDown(dropKey))
        {
            
        }
        if(itemObject != null)
        {
            itemObject.transform.position = objectPosition.position;
            itemObject.transform.rotation = objectPosition.rotation;
        }


    }

    
}
