using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthImpl : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;

    public abstract void TakeDamage(float damageAmount);
    public virtual void Healing(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void FullHealing(){
        Healing(maxHealth);
    }
}
