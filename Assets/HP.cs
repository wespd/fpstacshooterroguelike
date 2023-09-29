using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
   public float maxHealth;
   float currentHealth;

   public void Start()
   {
        currentHealth = maxHealth;
   }

   public void ChangeHealth(float change)
   {
        currentHealth += change;
        if(currentHealth <= 0)
        {
            OnDeath();
        }
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
   }
   public virtual void OnDeath()
   {
        Debug.Log(gameObject + " has died)");
   }
}
