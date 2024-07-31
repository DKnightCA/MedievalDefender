using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    // The enemy will shoot a GameObject. The speed and damage are determined by the bullet itself. 
    // This script only manages the shooting and instantiation of the object.
    // The enemy has an attack speed, subject to change. The attack speed is the time it takes the enemy to attack. Example: if the attack speed is 4, the enemy will 
    // attack every 4 seconds, meaning its attack speed is 0.25(1/4), its inverse.
    
    public GameObject bullet;
   // public Transform bulletTransform; //Commented line to check if game works without it.
    [SerializeField] public float attackDistance;
    public bool canShoot;
    public float attackSpeed;
    

    // public GameObject target; // Commented line to check if game works without it.
    private float timer;
    private Transform target;


    // Start is called before the first frame update
    // canShoot is set to false by default so that the enemy does not spawn a projectile upon spawning.
    void Start()
    {
        canShoot = false; 
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    // The enemy shoots automatically when the timer is higher than its attack speed, then the timer is set to 0
    // Every frame the enemy is not allowed to attack, it will update the timer until it can attack again.
    void Update()
    {
        if(canShoot && (Vector2.Distance(transform.position, target.position)) <= attackDistance)
        {
            canShoot = false;
            timer = 0;
            Instantiate(bullet, transform.position, Quaternion.identity);
        } else
        {
            timer += Time.deltaTime;
            if(timer > attackSpeed)
            {
                canShoot = true;
                timer = 0;
            }
        }
    }
}
