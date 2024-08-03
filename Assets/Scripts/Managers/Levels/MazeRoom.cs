using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRoom
{
    // The size of the room, in tiles.
    public int sizeX;
    public int sizeY;

    // Which borders of the room are doors. In order, connection points are: North, South, West, East.
    public bool[] connections;

    public int enemies;
    public GameObject[] enemyList;

    public MazeRoom(int x, int y)
    {
        sizeX = x;
        sizeY = y;
        connections = new bool[4]; // North, South, West, East
    }

    public MazeRoom()
    {
        connections = new bool[4];
        for (int i = 0; i < connections.Length; i++)
        {
            connections[i] = false;
        }
    }
}
