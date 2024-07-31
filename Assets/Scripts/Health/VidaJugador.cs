using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper;
using UnityEngine;
using UnityEngine.UI;

public class VidaJugador : HealthImpl
{
    public Image healthBar;
    public bool canBeDamaged;
    public float invincibilityTime;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color invincibilityColor;
    private float timer;
    public static event Action<VidaJugador> OnPlayerDeath;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    // The player has an invincibility time so that its health does not go to 0 directly.
    void Update()
    {
        if(!canBeDamaged)
        {
            timer += Time.deltaTime;
            if(timer > invincibilityTime)
            {
                canBeDamaged = true;
                sr.color = Color.white;
                
                timer = 0;
            }
        }
    }

    // This method is invoked by an attacker to reduce the player's current health.
    public override void TakeDamage(float damageAmount)
    {
        if(canBeDamaged)
        {
            currentHealth -= damageAmount;
            canBeDamaged = false;
            sr.color = invincibilityColor;
            healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
        }

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            OnPlayerDeath?.Invoke(this);
        }
    }

    // This method is invoked every time the player's current health increases. Current health cannot be higher than the max health
    public override void Healing(float healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
    }

    // This method is used ONLY when the max health of the player increases. TODO: assert that healthAdded cannot be < 1
    public void ChangeMaxHealth(float healthAdded)
    {
        if(healthAdded > 0)
        {
            maxHealth += healthAdded;
            currentHealth += healthAdded;
        }
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
    }


//  Método que queda sin usar. Es responsabilidad del atacante obtener la vida del objetivo.
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.tag == "Enemigo")
//        {
//            if(collision.gameObject.TryGetComponent<Enemigo>(out Enemigo enemyComponent))
//                    {
//                        TakeDamage(enemyComponent.dmg);
//                    }
//        }
//    }

}
