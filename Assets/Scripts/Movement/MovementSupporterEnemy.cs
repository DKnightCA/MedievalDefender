using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSupporterEnemy : MonoBehaviour
{

    // A supporter type enemy will move towards another enemy instead of the player.
    // If the enemy it is targeting dies, it will target another enemy.
    [SerializeField] public float moveSpeed;
    public float stopDistance; // Distance at which the enemy stops following.
    public float retreatDistance;
    private Transform target;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    

    void Start()
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Enemigo");   
        target = spawns[Random.Range((int) 0, (int) spawns.Length)].transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Enemigo").transform;
        }
        else
        {
            if(Vector2.Distance(transform.position, target.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            } else if(Vector2.Distance(transform.position, target.position) < stopDistance 
                && Vector2.Distance(transform.position, target.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, -moveSpeed * Time.deltaTime);
            }
        }
    }
}
