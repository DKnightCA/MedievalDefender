using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSpell : MonoBehaviour
{
    public float speed;
    public float dmg;

    private Rigidbody2D rb;
    protected Transform player;
    protected Vector2 target;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        rb = GetComponent<Rigidbody2D>();

        Vector3 direction = player.position - transform.position;
        Vector3 rotation = transform.position - player.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
    
   

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.TryGetComponent<VidaJugador>(out VidaJugador vidaJug))
        {
            vidaJug.TakeDamage(dmg);
        }
        Destroy(gameObject);
    }

    
    public void setDamage(float newDamage){
        this.dmg = newDamage;
    }
}
