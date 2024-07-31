using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing_effect : MonoBehaviour
{
    public float healAmount;
    public VidaEnemigo vidaEnem;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        healAmount = 1;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemigo")
        {
            vidaEnem = other.GetComponent(typeof(VidaEnemigo)) as VidaEnemigo;                    
            vidaEnem.Healing(healAmount);
        }
    }

/*
    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Enemigo")
        {
            vidaEnem = other.GetComponent(typeof(VidaEnemigo)) as VidaEnemigo;                    
            vidaEnem.TakeDamage(healAmount);
        }
    }
*/
}
