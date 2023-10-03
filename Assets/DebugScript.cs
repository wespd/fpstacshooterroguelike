using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{

    public KeyCode reset;
    public Transform resetPos;
    public Rigidbody rB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(reset))
        {
            transform.position = resetPos.position;
            rB.velocity = Vector3.zero;
        }
    }
}
