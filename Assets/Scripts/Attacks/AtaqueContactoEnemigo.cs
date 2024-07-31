using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueContactoEnemigo : MonoBehaviour
{   
    // A close ranged enemy has damage based on contact, and can access the player's health script to damage it.
    public HealthImpl vidaJug;

    public float damage { get; set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Enemies cannot be passed through, hence the method being Collision.
    // Upon collision, the enemy will access the player's life script and use its method to substract its health according to the enemy's damage.
    // Enemies are only able to damage the player 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            vidaJug = collision.gameObject.GetComponent(typeof(HealthImpl)) as HealthImpl;
            vidaJug.TakeDamage(damage);
        }
    }

    // The enemy should still damage the player while mantaining contact. This is to prevent the player from being invincible if it does not gets away from the enemy.
    private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                vidaJug = collision.gameObject.GetComponent(typeof(HealthImpl)) as HealthImpl;
                CauseDamage(vidaJug);
            }
        }

    public void CauseDamage(HealthImpl target)
    {
        target.TakeDamage(damage);
    }

    public void ChangeDamage(float dmgChange)
    {
        // TODO 
    }



    // This methods are unused, but left on the code, in case some enemy should use triggers
    //    private void OnTriggerEnter2D(Collider2D collider)
    //    {
    //        if (collider.tag == "Player")
    //        {
    //            vidaJug = collider.gameObject.GetComponent(typeof(VidaJugador)) as VidaJugador;
    //            vidaJug.TakeDamage(dmg);
    //        }
    //    }

    //    private void OnTriggerStay2D(Collider2D collider)
    //    {
    //        if (collider.tag == "Player")
    //        {
    //            vidaJug = collider.gameObject.GetComponent(typeof(VidaJugador)) as VidaJugador;
    //            vidaJug.TakeDamage(dmg);
    //        }
    //    }
}
