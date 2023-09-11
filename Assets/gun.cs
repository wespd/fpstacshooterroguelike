using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : Item
{
    RaycastHit hit;
    
    public float shotRange;

    public float damage;

    public override void Use()
    {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shotRange);
        if(hit.collider != null)
        {
            HP hitHealth = hit.collider.GetComponent<HP>();
            if(hitHealth != null)
            {
                Debug.Log(hit.collider);
                hitHealth.ChangeHealth(-damage);
            }
        }
    }
}
