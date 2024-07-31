using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShield : MonoBehaviour
{

    [SerializeField] private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(3F, 2.5F, 1);
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = Color.Lerp(Color.white, Color.blue, Mathf.PingPong(Time.time, 1)); 
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(other.gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemySpell")
        {
            Destroy(other.gameObject);
        }
    }
}
