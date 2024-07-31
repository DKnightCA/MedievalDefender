using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceDrop : MonoBehaviour
{
    public int experience;

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
        Debug.Log("HandleEnemyDefeated");
        if (defeatedEnemy != this.gameObject)
        {
            Debug.Log("incorrect");
            return;
        }
        Debug.Log("CORRECTO");
        PlayerDataManager.Instance.AddExperience(experience);
    }
}
