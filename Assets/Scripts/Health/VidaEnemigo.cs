using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaEnemigo : HealthImpl
{
    // Like the player, an enemy has a current health and a max health

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public override void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            EventManager.EnemyDefeated(this.gameObject);
            Destroy(gameObject);  
        }
    }
}
