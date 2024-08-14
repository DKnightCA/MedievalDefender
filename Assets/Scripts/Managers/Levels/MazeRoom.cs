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
    private Vector2Int tilemapStart;

    // Which borders of the room are doors. In order, connection points are: North, South, West, East.
    public bool[] connections;

    public int enemies; // Max number of enemies that can spawn in this room
    public GameObject[] roomEnemies; // Type of enemies that can spawn in this room
    protected GameObject[] instantiatedEnemies; // Array to keep track of the spawned enemies
    
    // Not in use currently
    public GameObject roomObjects;
    private GameObject instantiatedObject;
    

    protected bool isCleared = false; // Indicates if the room has been already cleared

    void Awake()
    {
        connections = new bool[4];
        for (int i = 0; i < connections.Length; i++)
        {
            connections[i] = false;
        }
        Camera mainCamera = Camera.main;
        sizeWorldY = 2f * Camera.main.orthographicSize;
        sizeWorldX = sizeWorldY * Camera.main.aspect;
    }

    protected void Start()
    {
        floorTilemap = GameObject.Find("Suelo").GetComponent<Tilemap>();
        wallsTilemap = GameObject.Find("Paredes").GetComponent<Tilemap>();
        CreateTilemap();
    }

    // Initializes the elements of the room when the player enters.
    public virtual void EnterRoom()
    {
        SpawnEnemies(40);
    }

    public virtual void ExitRoom()
    {
        DespawnEnemies();
    }

    protected GameObject[] SpawnEnemies(int spawnChance)
    {
        Vector2[] spawnZone = GetSpawnZone();
        Vector2 spawnPoint;
        instantiatedEnemies = new GameObject[enemies];

        // Gets a spawn point and a random enemy to instantiate. Every enemy has a chance to spawn or not.
        for (int i = 0; i < enemies; i++)
        {
            spawnPoint = new Vector2(Random.Range(spawnZone[0].x, spawnZone[1].x), Random.Range(spawnZone[0].y, spawnZone[1].y));
            int randomEnemy = Random.Range(0, roomEnemies.Length);
            if (Random.Range(0, 100) < spawnChance)
            {
                instantiatedEnemies[i] = SpawnEnemy(roomEnemies[randomEnemy], spawnPoint); 
                StartCoroutine(DisableEnemyMovement(instantiatedEnemies[i]));
            }
        }
        return instantiatedEnemies;
    }

    protected GameObject SpawnEnemy(GameObject enemy, Vector2 spawnPoint)
    {
        return Instantiate(enemy, spawnPoint, Quaternion.identity, transform);
    }

    private IEnumerator DisableEnemyMovement(GameObject enemy)
    {
        MovimientoEnemigo movimiento = enemy.GetComponent<MovimientoEnemigo>();
     //   Debug.Log("movimienti " + movimiento);
        float originalSpeed = movimiento.moveSpeed;
        movimiento.moveSpeed = 0;

        yield return new WaitForSeconds(0.5f);
        movimiento.moveSpeed = originalSpeed;

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
        Vector2 enemiesSpawnZone = pointZero + new Vector2(sizeTileX -5, sizeTileY -5);

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
            pointZero.x += 3;
            enemiesSpawnZone.x += 1;
        }
        if (connections[3])
        {
            pointZero.x -= 1;
            enemiesSpawnZone.x -= 3;
        }
        Vector2[] result = new Vector2[2];
        result[0] = pointZero;
        result[1] = enemiesSpawnZone;
        return result;
    }

    public void RoomCleared()
    {
        isCleared = true;
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

    // +++++++++++++++++++++++++++ TILEMAP +++++++++++++++++++++++++++++++++++
    public void CreateTilemap()
    {
        CreateFloorTilemap();
        CreateWallsTilemap();
        CreateRoomBorderTilemap();
        ConnectRoomsTilemap();
    }

    public void CreateFloorTilemap()
    {
        Vector2Int tilemapStart = GetTilemapStart();
        for (int x = tilemapStart.x; x <= tilemapStart.x + sizeTileX-1; x++)
        {
            for (int y = tilemapStart.y; y <= tilemapStart.y + sizeTileY-1; y++)
            {
                floorTilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
            }
        }
    }

    public void CreateWallsTilemap()
    {
        Vector2Int tilemapStart = GetTilemapStart();
        for (int x = tilemapStart.x; x <= tilemapStart.x + sizeTileX-1; x++)
        {
            if ((x == tilemapStart.x || x == tilemapStart.x + sizeTileX-1))
            {
                for (int y = tilemapStart.y; y <= tilemapStart.y + sizeTileY-1; y++)
                {
                    wallsTilemap.SetTile(new Vector3Int(x, y, 0), wallTile);
                }
            }
        }

        for (int y = tilemapStart.y; y <= tilemapStart.y + sizeTileY-1; y++)
        {
            if ((y == tilemapStart.y || y == tilemapStart.y + sizeTileY-1))
            {
                for (int x = tilemapStart.x; x <= tilemapStart.x + sizeTileX-1; x++)
                {
                    wallsTilemap.SetTile(new Vector3Int(x, y, 0), wallTile);
                }
            }
        }

    }

    public void CreateRoomBorderTilemap()
    {
        Vector2Int tilemapStart = GetTilemapStart();
        for (int x = tilemapStart.x - 1; x <= tilemapStart.x + sizeTileX; x++)
        {
            floorTilemap.SetTile(new Vector3Int(x, tilemapStart.y-1, 0), floorTile);
            floorTilemap.SetTile(new Vector3Int(x, tilemapStart.y + sizeTileY, 0), floorTile);

        }

        for (int y = tilemapStart.y - 1; y <= tilemapStart.y + sizeTileY; y++)
        {
            floorTilemap.SetTile(new Vector3Int(tilemapStart.x-1, y, 0), floorTile);
            floorTilemap.SetTile(new Vector3Int(tilemapStart.x + sizeTileX, y, 0), floorTile);
        }
    }

    public void ConnectRoomsTilemap()
    {
        Vector2Int tilemapStart = GetTilemapStart();
        if (connections[0])
        {

            int realX = tilemapStart.x + sizeTileX / 2;
            int realY = tilemapStart.y + sizeTileY -1;
            wallsTilemap.SetTile(new Vector3Int(realX, realY, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX - 1, realY, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX + 1, realY + 1, 0), wallTile);
            wallsTilemap.SetTile(new Vector3Int(realX - 2, realY + 1, 0), wallTile);
        }
        if (connections[1])
        {
            int realX = tilemapStart.x + sizeTileX /2;
            int realY = tilemapStart.y;
            wallsTilemap.SetTile(new Vector3Int(realX, realY, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX - 1, realY, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX + 1, realY - 1, 0), wallTile);
            wallsTilemap.SetTile(new Vector3Int(realX - 2, realY - 1, 0), wallTile);
        }
        if (connections[2])
        {
            int realX = tilemapStart.x;
            int realY = tilemapStart.y + sizeTileY / 2;
            wallsTilemap.SetTile(new Vector3Int(realX, realY, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX, realY - 1, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX - 1, realY + 1, 0), wallTile);
            wallsTilemap.SetTile(new Vector3Int(realX - 1, realY - 2, 0), wallTile);
        }
        if (connections[3])
        {
            int realX = tilemapStart.x + sizeTileX -1;
            int realY = tilemapStart.y + sizeTileY / 2;
            wallsTilemap.SetTile(new Vector3Int(realX, realY, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX, realY - 1, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX + 1, realY + 1, 0), wallTile);
            wallsTilemap.SetTile(new Vector3Int(realX + 1, realY - 2, 0), wallTile);

        }
    }

    public void CloseRoomTilemap()
    {
        Vector2Int tilemapStart = GetTilemapStart();
        if (connections[0])
        {

            int realX = tilemapStart.x + sizeTileX / 2;
            int realY = tilemapStart.y + sizeTileY - 1;
            wallsTilemap.SetTile(new Vector3Int(realX, realY, 0), wallTile);
            wallsTilemap.SetTile(new Vector3Int(realX - 1, realY, 0), wallTile);
        }
        if (connections[1])
        {
            int realX = tilemapStart.x + sizeTileX / 2;
            int realY = tilemapStart.y;
            wallsTilemap.SetTile(new Vector3Int(realX, realY, 0), wallTile);
            wallsTilemap.SetTile(new Vector3Int(realX - 1, realY, 0), wallTile);
        }
        if (connections[2])
        {
            int realX = tilemapStart.x;
            int realY = tilemapStart.y + sizeTileY / 2;
            wallsTilemap.SetTile(new Vector3Int(realX, realY, 0), wallTile);
            wallsTilemap.SetTile(new Vector3Int(realX, realY - 1, 0), wallTile);
        }
        if (connections[3])
        {
            int realX = tilemapStart.x + sizeTileX - 1;
            int realY = tilemapStart.y + sizeTileY / 2;
            wallsTilemap.SetTile(new Vector3Int(realX, realY, 0), wallTile);
            wallsTilemap.SetTile(new Vector3Int(realX, realY - 1, 0), wallTile);

        }
    }

    protected Vector2Int GetTilemapStart()
    {
        float roomDistanceX = this.transform.position.x / sizeWorldX;
        float roomDistanceY = this.transform.position.y / sizeWorldY;
        float roomStartX = roomDistanceX * sizeTileX + roomDistanceX - (sizeTileX/2);
        float roomStartY = roomDistanceY * sizeTileY + roomDistanceY - (sizeTileY/2);
        return new Vector2Int((int)roomStartX, (int)roomStartY);
    }

}