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

    // Initializes the elements of the room when the player enters.
    public virtual void EnterRoom()
    {
        SpawnEnemies();
    }

    public virtual void ExitRoom()
    {
        DespawnEnemies();
    }

    // The first entry of the array is the bottom left corner of the spawn area, and the second entry is the top right corner.
    private void SpawnEnemies()
    {
        Vector2[] spawnZone = GetSpawnZone();
        Vector2 spawnPoint;
        instantiatedEnemies = new GameObject[enemies];

        for (int i = 0; i < enemies; i++)
        {
            spawnPoint = new Vector2(Random.Range(spawnZone[0].x, spawnZone[1].x), Random.Range(spawnZone[0].y, spawnZone[1].y));
            instantiatedEnemies[i] = Instantiate(roomEnemies[0], spawnPoint, Quaternion.identity, transform);
        }
    }

    private void DespawnEnemies()
    {
        if(instantiatedEnemies == null)
        {
            return;
        }
        foreach (GameObject enemy in instantiatedEnemies)
        {
            Destroy(enemy);
        }
    }
            
    private Vector2[] GetSpawnZone()
    {
        Vector2 pointZero = new Vector2(this.transform.position.x - sizeTileX/2 +2,
                                        this.transform.position.y - sizeTileY/2 +2);
        Vector2 enemiesSpawnZone = pointZero + new Vector2(sizeTileX -4, sizeTileY -4);
        Debug.Log("pointZero: " + pointZero);
        Debug.Log(enemiesSpawnZone);

        // Adjust the spawn area of the enemies to be fair for the player.
        if (connections[0])
        {
            pointZero.y -= 1;
            enemiesSpawnZone.y -= 1.5f;
        }
        if (connections[1])
        {
            pointZero.y += 1.5f;
            enemiesSpawnZone.y += 1;
        }
        if (connections[2])
        {
            pointZero.x += 1.5f;
            enemiesSpawnZone.x += 1;
        }
        if (connections[3])
        {
            pointZero.x -= 1;
            enemiesSpawnZone.y -= 1.5f;
        }
        Vector2[] result = new Vector2[2];
        result[0] = pointZero;
        result[1] = enemiesSpawnZone;
        return result;
    }

    public bool IsDeadEnd()
    {
        int connectionCount = 0;
        foreach (bool con in connections)
        {
            if (con) { connectionCount++; }
        }
        if(connectionCount == 1)
        {
            return true;
        }
        return false;
    }
}