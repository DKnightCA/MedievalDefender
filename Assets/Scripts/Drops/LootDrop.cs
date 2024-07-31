using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LootDrop : MonoBehaviour
{
    public GameObject[] loot;
    [SerializeField] int probabilityDrop; // The probability that a monster drops loot
    int randomLoot;

    void OnEnable()
    {
        EventManager.OnEnemyDefeated += HandleEnemyDefeated;
    }

    void OnDisable()
    {
        EventManager.OnEnemyDefeated -= HandleEnemyDefeated;
    }

    public void HandleEnemyDefeated(GameObject defeatedEnemy)
    {
        if(defeatedEnemy != this.gameObject){
            return;
        }

        if(UnityEngine.Random.Range((int) 0, (int) 101) < probabilityDrop)
        {
            randomLoot = UnityEngine.Random.Range(0, loot.Length);
            Instantiate(loot[randomLoot], transform.position, Quaternion.identity);
        }
    }
}
