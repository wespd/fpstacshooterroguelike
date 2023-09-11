using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public Item currentItem;
    public GameObject itemObject;

    public float pickupRange;

    public KeyCode pickupKey = KeyCode.E;

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
                    itemObject = hitItem.itemObject;
                }
            }
        }
    }

    
}
