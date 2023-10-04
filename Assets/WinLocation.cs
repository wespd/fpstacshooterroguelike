using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinLocation : MonoBehaviour
{
    public UnityEvent onWin;
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<movement>() != null)
        {
            onWin.Invoke();
        }  
    }
}
