using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hechizo_hielo : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float dmg;

    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float x = Mathf.Cos(transform.localEulerAngles.z * Mathf.Deg2Rad);
        float y = Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad);
        rb.velocity = new Vector2(x, y).normalized * speed;
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<VidaJugador>(out VidaJugador vidaJug))
        {
            vidaJug.TakeDamage(dmg);
        }
        Destroy(gameObject);
    }
}
