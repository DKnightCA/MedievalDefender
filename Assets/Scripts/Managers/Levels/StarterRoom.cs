using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterRoom : MazeRoom
{

    new void Start()
    {
        base.Start();
        Instantiate(roomObjects, this.transform);
    }
    public override void EnterRoom(){}

    public override void ExitRoom(){}

    
}
