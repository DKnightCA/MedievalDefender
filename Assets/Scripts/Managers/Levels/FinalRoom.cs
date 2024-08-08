using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoom : MazeRoom
{
    private Countdown countdown;
    private MonsterSpawner spawner;
    private bool roomCleared = false;
    // Start is called before the first frame update

    void Start()
    {
        EventManager.OnLevelPassed += RoomCleared;
        base.Start();
        countdown = this.gameObject.GetComponent<Countdown>();

        countdown.StopCountdown();
        spawner = this.gameObject.GetComponent<MonsterSpawner>();
        spawner.spawnArea[0] = GetTilemapStart() + new Vector2Int(1,1);
        spawner.spawnArea[1] = GetTilemapStart() + new Vector2Int(sizeTileX - 1, sizeTileY - 1);
    }
    public override void EnterRoom() {
        if (!roomCleared)
        {
            CloseRoomTilemap();
            countdown.StartCountdown();
            spawner.Activate();
        }
        // Start boosfight or timed fight or whatever.
    }


    public void RoomCleared()
    {   
        roomCleared = true;
        ConnectRoomsTilemap();
    }

    public override void ExitRoom() { }
}
