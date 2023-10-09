using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void InteractTrigger(Interact other)
    {
        Debug.Log("YOU HIT A BASIC INTERACTABLE");
    }
}
