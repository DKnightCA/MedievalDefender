using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeRoom : MonoBehaviour
{
    // The size of the room, in tiles.
    public Tilemap floorTilemap;
    public Tilemap wallsTilemap;
    public TileBase floorTile;
    public TileBase wallTile;
    public int sizeTileX = 18;
    public int sizeTileY = 10;

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
        Camera mainCamera = Camera.main;
        sizeWorldX = 2f * Camera.main.orthographicSize;
        sizeWorldY = sizeWorldX * Camera.main.aspect;
    }
    void Start()
    {
        Instantiate(objeto, transform);
        EventManager.OnEnterRoom += EnterRoom;
        EventManager.OnExitRoom += ExitRoom;

    }

    // Initializes the elements of the room when the player enters.
    public void EnterRoom(MazeRoom room)
    {
        // Instantiate enemies and objects
    }

    public void ExitRoom(MazeRoom room)
    {
        // Destroy all enemies
    }

}