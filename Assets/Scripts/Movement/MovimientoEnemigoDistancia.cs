using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigoDistancia : MonoBehaviour
{
    // An enemy who attacks from a distance will move towards the player, but it will stop after a certain distance.
    // If the enemy is too close to the player, it will move in the opposite direction.
    [SerializeField] public float moveSpeed;
    private Transform target;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    public float stopDistance; // Distance at which the enemy stops following.
    public float retreatDistance; // Distance at which the enemy starts retreating.
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        } else if(Vector2.Distance(transform.position, target.position) < stopDistance
                    && Vector2.Distance(transform.position, target.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        } else if(Vector2.Distance(transform.position, target.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -moveSpeed * Time.deltaTime);
        }
    }

}
