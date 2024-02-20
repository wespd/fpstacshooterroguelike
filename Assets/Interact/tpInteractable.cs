using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpInteractable : Interactable
{
    public override void InteractTrigger(Interact other)
    {
        Vector3 temp = other.gameObject.transform.position;
        other.gameObject.transform.position = transform.position;
        transform.position = temp;
    }
}
