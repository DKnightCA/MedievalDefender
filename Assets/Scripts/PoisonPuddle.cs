using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPuddle : MonoBehaviour
{

    public float puddleTime;
    public float dmg;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= puddleTime)
        {
            Destroy(gameObject);      
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<VidaJugador>(out VidaJugador vidaJug))
        {
            vidaJug.TakeDamage(dmg);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.TryGetComponent<VidaJugador>(out VidaJugador vidaJug))
        {
            vidaJug.TakeDamage(dmg);
        }
    }
}