using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleSpell : NormalSpell
{
    public GameObject endOfSpell;
    public float timeToLive;
    private float timer;
    // Start is called before the first frame update

    // Update is called once per frame
    protected override void Start()
    {
        base.Start();
        Vector2.MoveTowards(transform.position, player.position, speed);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >=  timeToLive)
        {
            Instantiate(endOfSpell, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        Instantiate(endOfSpell, transform.position, Quaternion.identity);
    }   
}
