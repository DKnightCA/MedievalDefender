using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<GameObject> OnEnemyDefeated;




    public static void EnemyDefeated(GameObject enemy)
    {
        OnEnemyDefeated?.Invoke(enemy);
    }
}
