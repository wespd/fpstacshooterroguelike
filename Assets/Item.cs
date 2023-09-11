using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject itemObject => gameObject;
    public virtual void Use()
    {
        Debug.Log(gameObject + " used");
    }
}
