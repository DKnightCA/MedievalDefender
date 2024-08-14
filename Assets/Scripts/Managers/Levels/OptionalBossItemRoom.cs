using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionalBossItemRoom : MazeRoom

{
    new void Start()
    {
        base.Start();
        
    }

    public override void EnterRoom()
    {
        if (isCleared) { return; }
        CloseRoomTilemap();
    }
    

}
