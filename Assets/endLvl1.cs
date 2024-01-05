using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endLvl1 : MonoBehaviour
{
    public Transform playerTransform;
    public Transform level2Location;
    public Rigidbody rB;
    public void moveLocation() 
    {
        playerTransform.position = level2Location.position;
        rB.velocity = Vector3.zero;
    }
}
