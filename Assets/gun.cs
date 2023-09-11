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
        HP hitHealth = hit.collider.GetComponent<HP>();
        if(hitHealth != null)
        {
            hitHealth.ChangeHealth(-damage);
        }
    }
}
