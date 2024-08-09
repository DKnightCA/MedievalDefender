using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoom : MazeRoom
{
    private bool roomCleared = false;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        EventManager.OnEnemyDefeated += CheckEnemies;
    }

    public override void EnterRoom()
    {
        if (!roomCleared)
        {
            CloseRoomTilemap();
            SpawnEnemies();
        }
        // Start boosfight or timed fight or whatever.
    }

    public void RoomCleared()
    {
        roomCleared = true;
        ConnectRoomsTilemap();
    }

    public override void ExitRoom() { }

    private void CheckEnemies(GameObject defeatedEnemy)
    {
        if (roomCleared || instantiatedEnemies == null)
        {
            return;
        }
        int enemiesRemaining = 0;
        foreach (GameObject enemy in instantiatedEnemies)
        {
            if (enemy != null)
            {
                enemiesRemaining++;
            }
        }
        if (enemiesRemaining == 1)
        {
            RoomCleared();
        }
    }
}
