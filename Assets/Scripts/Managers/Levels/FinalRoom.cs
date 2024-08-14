using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoom : MazeRoom
{
    private Countdown countdown;
    private MonsterSpawner spawner;
    // Start is called before the first frame update

    new void Start()
    {
        base.Start();
        EventManager.OnLevelPassed += RoomCleared;
        countdown = this.gameObject.GetComponent<Countdown>();

        countdown.StopCountdown();
        spawner = this.gameObject.GetComponent<MonsterSpawner>();
        spawner.spawnArea[0] = GetTilemapStart() + new Vector2Int(1,1);
        spawner.spawnArea[1] = GetTilemapStart() + new Vector2Int(sizeTileX - 1, sizeTileY - 1);
        spawner.Deactivate();
    }
    public override void EnterRoom() {
        if (!isCleared)
        {
            CloseRoomTilemap();
            countdown.StartCountdown();
            spawner.Activate();
        }
        // Start boosfight or timed fight or whatever.
    }

    public new void RoomCleared()
    {
        base.RoomCleared();
        ConnectRoomsTilemap();
    }

    public override void ExitRoom() { }
}
