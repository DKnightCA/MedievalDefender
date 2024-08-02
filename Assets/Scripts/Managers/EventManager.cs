using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<GameObject> OnEnemyDefeated;
    public static event Action OnLevelPassed;
    public static event Action OnTogglePauseMenu;
   


    public static void EnemyDefeated(GameObject enemy)
    {
        OnEnemyDefeated?.Invoke(enemy);
    }

    public static void LevelPassed()
    {
        OnLevelPassed?.Invoke();
    }

    public static void TogglePauseMenu()
    {
        OnTogglePauseMenu?.Invoke();
    }

}
