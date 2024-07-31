using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    private Vector3 posRaton;
    private Camera camaraPrincipal;
    private Rigidbody2D rb;
    public float fuerza;
    public float dmg;

    [SerializeField] public AudioClip ArrowShot;

    // Start is called before the first frame update
    void Start()
    {
        camaraPrincipal = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        posRaton = camaraPrincipal.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direccion = posRaton - transform.position;
        Vector3 rotacion = transform.position - posRaton;
        rb.velocity = new Vector2(direccion.x, direccion.y).normalized * fuerza;
        float rot = Mathf.Atan2(rotacion.y, rotacion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);

        ControladorSonido.Instance.playSound(ArrowShot);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        // Busca el componente script Enemigo
        if(collision.gameObject.TryGetComponent<VidaEnemigo>(out VidaEnemigo enemyComponent))
        {
            enemyComponent.TakeDamage(dmg);
        }
        Destroy(gameObject);
    }

    //Por ahora este método queda sin usar. Vamos a probar con Colliders en vez de Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<VidaEnemigo>(out VidaEnemigo enemyComponent))
        {
            enemyComponent.TakeDamage(dmg);
        }
   }

    // Destruye el objeto al salir de la pantalla
    void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
}
