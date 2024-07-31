using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupporterEnemyAction : MonoBehaviour
{
    public GameObject supportSpawn;
    public bool canAttack;
    public float attackSpeed;
    public int spawnCount;
    public Transform[] spawnPoints;
    
    private float timer;
    private Rigidbody2D rb;
    private Transform player;
    private Vector2 target;
    private int randomPoint;
    [SerializeField] private bool random;
    // Start is called before the first frame update
    void Start()
    {
        canAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canAttack)
        {
            if(random)
            {
                SupportAttackRandom();
            }
            else
            {
                SupportAttackAll();
            }
        }
        else {
            timer+= Time.deltaTime;
            if(timer > attackSpeed)
            {
                canAttack = true;
                timer = 0;
            }
        }
    }

    void SupportAttackRandom()
    {
        for(int i = 0; i < spawnCount; i++)
        {
            randomPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(supportSpawn, spawnPoints[randomPoint].position, Quaternion.identity);
        }
        canAttack = false;
    }

    void SupportAttackAll()
    {
        int rotation = 0;
        int j = spawnPoints.Length;
        for(int i = 0; i < j; i++)
        {
            GameObject ice = Instantiate(supportSpawn, spawnPoints[i].position, Quaternion.Euler(new Vector3(0, 0, rotation)));
            rotation += 45;
            // Rigidbody2D bullet = ice.GetComponent<Rigidbody2D>();
            // bullet.velocity = spawnPoints[i].position - transform.position;
        }
        canAttack = false;
    }

    void SupportSummoner()
    {
        
    }
}
