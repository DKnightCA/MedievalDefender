using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Vector2[] spawnArea;
    public GameObject[] enemies;
    int randomEnemy;
    private bool spawnAllowed;
    public bool isActive;
    public float timeBetweenSpawns;
    private float timer;

    private List<GameObject> spawnedEnemies;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnCountdownEnd += Deactivate;
        EventManager.OnEnemyDefeated += CheckEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            SpawnAMonster();
        }
    }

    void SpawnAMonster()
    {
        if(spawnAllowed)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(spawnArea[0].x, spawnArea[1].x), Random.Range(spawnArea[0].y, spawnArea[1].y), 0);
            randomEnemy = Random.Range(0, enemies.Length);
            GameObject newEnemy = Instantiate(enemies[randomEnemy], spawnPoint, Quaternion.identity);
            spawnedEnemies.Add(newEnemy);
            spawnAllowed = false;
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenSpawns)
            {
                spawnAllowed = true;
                timer = 0;
            }
        }
    }

    public void Deactivate()
    {
        isActive = false;
    }

    public void Activate()
    {
        isActive = true;
        spawnedEnemies = new List<GameObject>();
    }

    private void CheckEnemies(GameObject defeatedEnemy)
    {
        Debug.LogError(spawnedEnemies);
        if (isActive || spawnedEnemies == null)
        {
            return;
        }

        spawnedEnemies.RemoveAll(enemy => enemy == null);
        if (spawnedEnemies.Count == 1)
        {
            EventManager.LevelPassed();
        }
    }
}
