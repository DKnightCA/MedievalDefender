using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

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
    public GameObject[] roomEnemies;
    private GameObject[] instantiatedEnemies;
    public GameObject roomObjects;
    private GameObject instantiatedObject;

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
        //Instantiate(roomObjects, transform);
        EventManager.OnEnterRoom += EnterRoom;
        EventManager.OnExitRoom += ExitRoom;
        EnterRoom(this);

    }

    // Initializes the elements of the room when the player enters.
    public void EnterRoom(MazeRoom room)
    {
        Vector2 enemiesSpawnZone = new Vector2(sizeWorldX, sizeWorldY);
        Vector2 spawnPoint;
        System.Random random = new System.Random();
        instantiatedEnemies = new GameObject[enemies];
        if (connections[0])
        {
            enemiesSpawnZone.x *= 0.7f;
        }
        if (connections[1])
        {
            enemiesSpawnZone.x *= 0.7f;
        }
        if (connections[2])
        {
            enemiesSpawnZone.y *= 0.7f;
        }
        if (connections[3])
        {
            enemiesSpawnZone.y *= 0.7f;
        }

        for (int i = 0; i < enemies; i++) {
            spawnPoint = new Vector2(random.Next((int)enemiesSpawnZone.x), random.Next((int)enemiesSpawnZone.y));
            instantiatedEnemies[i] = Instantiate(roomEnemies[0], new Vector3(enemiesSpawnZone.x, enemiesSpawnZone.y, 0), Quaternion.identity, transform);
        }
    }

    public void ExitRoom(MazeRoom room)
    {
        // Destroy all enemies
    }

}