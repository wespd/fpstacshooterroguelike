using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpInteractable : Interactable
{
    public override void InteractTrigger(Interact other)
    {
        other.gameObject.transform.position = transform.position;
    }
}
