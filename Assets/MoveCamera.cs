using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform position;
    void Update()
    {
        transform.position = position.position;
    }
}
