using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemies;
    int randomSpawnPoint;
    int randomEnemy;
    public static bool spawnAllowed;
    public float timeBetweenSpawns;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        spawnAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawnAllowed)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenSpawns)
            {
                spawnAllowed = true;
                timer = 0;
            }
        }
        SpawnAMonster();

    }

    void SpawnAMonster()
    {
        if(spawnAllowed)
        {
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            randomEnemy = Random.Range(0, enemies.Length);
            Instantiate(enemies[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            spawnAllowed = false;
        }
    }
}
