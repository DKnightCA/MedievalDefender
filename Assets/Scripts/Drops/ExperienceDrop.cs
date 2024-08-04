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
        if (defeatedEnemy != this.gameObject)
        {
            return;
        }
        PlayerDataManager.Instance.AddExperience(experience);
    }
}
