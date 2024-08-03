using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRoom : MonoBehaviour
{
    // The size of the room, in tiles.
    public int sizeTileX;
    public int sizeTileY;

    public float sizeWorldX;
    public float sizeWorldY;

    // Which borders of the room are doors. In order, connection points are: North, South, West, East.
    public bool[] connections;

    public int enemies;
    public GameObject[] enemyList;
    public GameObject objeto;

    void Awake()
    {
        connections = new bool[4];
        for (int i = 0; i < connections.Length; i++)
        {
            connections[i] = false;
        }
    }
    void Start()
    {
        Camera mainCamera = Camera.main;
        sizeWorldX = mainCamera.rect.x;
        sizeWorldY = mainCamera.rect.y;
        Instantiate(objeto, transform);
    }

    // Initializes the elements of the room when the player enters.
    public void EnterRoom()
    {

    }
}
