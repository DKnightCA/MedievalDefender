using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterRoom : MazeRoom
{

    void Start()
    {
        Instantiate(roomObjects, this.transform);
    }
    public override void EnterRoom(){}

    public override void ExitRoom(){}

    
}
