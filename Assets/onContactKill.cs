using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onContactKill : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        HP hP = other.GetComponent<HP>();
        if(hP != null)
        {
            hP.OnDeath();
        }
    }
}
