using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoom : MazeRoom
{

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        EventManager.OnEnemyDefeated += CheckEnemies;
    }

    public override void EnterRoom()
    {
        if (!isCleared)
        {
            CloseRoomTilemap();
            SpawnEnemies(100);
        }
        // Start boosfight or timed fight or whatever.
    }

    public new void RoomCleared()
    {
        base.RoomCleared();
        ConnectRoomsTilemap();
        Instantiate(roomObjects, this.transform);
    }

    public override void ExitRoom() { }

    private void CheckEnemies(GameObject defeatedEnemy)
    {
        if (isCleared || instantiatedEnemies == null)
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
