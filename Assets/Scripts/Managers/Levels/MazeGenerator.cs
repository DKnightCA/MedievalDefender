using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using System;

public class MazeGenerator : MonoBehaviour
{
    public int sizeX;
    public int sizeY;
    public Vector2Int starterRoom;
    public Vector2Int finalRoom;

    public GameObject roomLimitPrefab;
    public GameObject roomPrefab;
    public MazeRoom[,] rooms;
    public Vector2 roomSize;
    public MazeRoom activeRoom;
    public Vector2Int activeRoomPosition;   

    private Vector2Int tilemapStart = new Vector2Int(-9, -5);
    private Vector2Int tilemapEnd = new Vector2Int(8, 4);
    private Vector2Int spaceBetweenRooms = new Vector2Int(1, 1);
    private Vector2Int roomSizeInTiles = new Vector2Int(18, 10);
    public Tilemap floorTilemap;
    public Tilemap wallsTilemap;
    public TileBase floorTile;
    public TileBase wallTile;

    private void Awake()
    {
        EventManager.OnCameraGoDown += UpdateActiveRoomBottom;
        EventManager.OnCameraGoUp += UpdateActiveRoomTop;
        EventManager.OnCameraGoLeft += UpdateActiveRoomLeft;
        EventManager.OnCameraGoRight += UpdateActiveRoomRight;
    }

    void Start()
    {
        starterRoom = new Vector2Int(sizeX / 2, 0);
        finalRoom = new Vector2Int(sizeX / 2, sizeY - 1);
        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        roomSize = new Vector2(width, height);
        InitializeRooms();
        GenerateMazePath();
        //DebugPrintMaze();
        InstantiateRoomConnectors();
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                CreateTilemap(new Vector2Int(x, y));
            }
        }
    }

    void InitializeRooms()
    {
        rooms = new MazeRoom[sizeX, sizeY];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                GameObject newRoom = Instantiate(roomPrefab, RoomNumberToWorldPosition(new Vector2Int(x, y)), Quaternion.identity);
                newRoom.name = $"MazeRoom_{x}_{y}";
                //Debug.Log("POSITION: " + RoomNumberToPosition(new Vector2Int(x, y)).x + RoomNumberToPosition(new Vector2Int(x, y)).y);
                rooms[x, y] = newRoom.GetComponent<MazeRoom>();
            }
        }
        activeRoom = rooms[starterRoom.x, starterRoom.y];
        activeRoomPosition = starterRoom;
    }

    void GenerateMazePath()
    {
        // Define start and end points
        int startX = sizeX / 2;
        int startY = 0;
        int endX = sizeX / 2;
        int endY = sizeY - 1;

        // Create a stack for the DFS
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        stack.Push(new Vector2Int(startX, startY));

        // Create a set to keep track of visited rooms
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        visited.Add(new Vector2Int(startX, startY));

        // Directions: North, South, West, East
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),  // North
            new Vector2Int(0, -1), // South
            new Vector2Int(-1, 0), // West
            new Vector2Int(1, 0)   // East
        };

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Peek();
            List<Vector2Int> neighbors = new List<Vector2Int>();

            foreach (Vector2Int dir in directions)
            {
                Vector2Int neighbor = current + dir;

                if (neighbor.x >= 0 && neighbor.x < sizeX && neighbor.y >= 0 && neighbor.y < sizeY && !visited.Contains(neighbor))
                {
                    neighbors.Add(neighbor);
                }
            }

            if (neighbors.Count > 0)
            {
                Vector2Int chosenNeighbor = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
                visited.Add(chosenNeighbor);
                stack.Push(chosenNeighbor);

                // Create connection between current and chosenNeighbor
                CreateConnection(current, chosenNeighbor);
            }
            else
            {
                stack.Pop();
            }
        }

        // Ensure end room is connected
        Vector2Int endRoomPos = new Vector2Int(endX, endY);
        if (!visited.Contains(endRoomPos))
        {
            ConnectToPath(endRoomPos, visited);
        }
    }

    void CreateConnection(Vector2Int current, Vector2Int neighbor)
    {
        int xDiff = neighbor.x - current.x;
        int yDiff = neighbor.y - current.y;

        if (xDiff == 1)
        {
            rooms[current.x, current.y].connections[3] = true; // East
            rooms[neighbor.x, neighbor.y].connections[2] = true; // West
        }
        else if (xDiff == -1)
        {
            rooms[current.x, current.y].connections[2] = true; // West
            rooms[neighbor.x, neighbor.y].connections[3] = true; // East
        }
        else if (yDiff == 1)
        {
            rooms[current.x, current.y].connections[0] = true; // North
            rooms[neighbor.x, neighbor.y].connections[1] = true; // South
        }
        else if (yDiff == -1)
        {
            rooms[current.x, current.y].connections[1] = true; // South
            rooms[neighbor.x, neighbor.y].connections[0] = true; // North
        }
    }

    void InstantiateRoomConnectors()
    {
        GameObject limit;
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                if (rooms[x, y].connections[0])
                {
                    Vector3 position = RoomNumberToWorldPosition(new Vector2Int(x, y));
                    position.y += roomSize.y / 2;
                    limit = Instantiate(roomLimitPrefab, position, Quaternion.identity);
                    limit.transform.Find("TopLimit").gameObject.SetActive(true);
                    limit.transform.Find("BottomLimit").gameObject.SetActive(true);
                    limit.name = $"RoomLimit_{x}_{y}_N";

                }
                if (rooms[x, y].connections[3])
                {
                    Vector3 position = RoomNumberToWorldPosition(new Vector2Int(x, y));
                    position.x += roomSize.x / 2;
                    limit = Instantiate(roomLimitPrefab, position, Quaternion.identity);
                    limit.transform.Find("LeftLimit").gameObject.SetActive(true);
                    limit.transform.Find("RightLimit").gameObject.SetActive(true);
                    limit.name = $"RoomLimit_{x}_{y}_E";

                }
            }
        }
    }

    void ConnectToPath(Vector2Int endRoomPos, HashSet<Vector2Int> visited)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),  // North
            new Vector2Int(0, -1), // South
            new Vector2Int(-1, 0), // West
            new Vector2Int(1, 0)   // East
        };

        foreach (Vector2Int dir in directions)
        {
            Vector2Int neighbor = endRoomPos + dir;

            if (neighbor.x >= 0 && neighbor.x < sizeX && neighbor.y >= 0 && neighbor.y < sizeY && visited.Contains(neighbor))
            {
                neighbors.Add(neighbor);
            }
        }

        if (neighbors.Count > 0)
        {
            Vector2Int chosenNeighbor = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
            CreateConnection(endRoomPos, chosenNeighbor);
            visited.Add(endRoomPos);
        }
    }

    void CreateTilemap(Vector2Int roomPosition)
    {
        CreateFloorTilemap(roomPosition);
        CreateWallsTilemap(roomPosition);
        CreateRoomBorderTilemap(roomPosition);
        ConnectRoomsTilemap(roomPosition);
        
    }

    private void CreateFloorTilemap(Vector2Int roomPosition)
    {
        for (int x = tilemapStart.x; x <= tilemapEnd.x; x++)
        {
            for (int y = tilemapStart.y; y <= tilemapEnd.y; y++)
            {
                Vector2Int distanceToStarterRoom = roomPosition - starterRoom;
                int realX = x + (distanceToStarterRoom.x * roomSizeInTiles.x) + (distanceToStarterRoom.x * spaceBetweenRooms.x);
                int realY = y + (distanceToStarterRoom.y * roomSizeInTiles.y) + (distanceToStarterRoom.y * spaceBetweenRooms.y);
                floorTilemap.SetTile(new Vector3Int(realX, realY, 0), floorTile);
            }
        }
    }

    private void CreateWallsTilemap(Vector2Int roomPosition)
    {

        for (int x = tilemapStart.x; x <= tilemapEnd.x; x++)
        {
            if ((x == tilemapStart.x || x == tilemapEnd.x))
            {
                for (int y = tilemapStart.y; y <= tilemapEnd.y; y++)
                {
                    Vector2Int distanceToStarterRoom = roomPosition - starterRoom;
                    int realX = x + (distanceToStarterRoom.x * roomSizeInTiles.x) + (distanceToStarterRoom.x * spaceBetweenRooms.x);
                    int realY = y + (distanceToStarterRoom.y * roomSizeInTiles.y) + (distanceToStarterRoom.y * spaceBetweenRooms.y);
                    wallsTilemap.SetTile(new Vector3Int(realX, realY, 0), wallTile);
                }
            }
        }

        for (int y = tilemapStart.y; y <= tilemapEnd.y; y++)
        {
            if ((y == tilemapStart.y || y == tilemapEnd.y))
            {
                for (int x = tilemapStart.x; x <= tilemapEnd.x; x++)
                {
                    Vector2Int distanceToStarterRoom = roomPosition - starterRoom;
                    int realX = x + (distanceToStarterRoom.x * roomSizeInTiles.x) + (distanceToStarterRoom.x * spaceBetweenRooms.x);
                    int realY = y + (distanceToStarterRoom.y * roomSizeInTiles.y) + (distanceToStarterRoom.y * spaceBetweenRooms.y);
                    wallsTilemap.SetTile(new Vector3Int(realX, realY, 0), wallTile);
                }
            }
        }

    }

    private void ConnectRoomsTilemap(Vector2Int roomPosition)
    {
        MazeRoom selectedRoom = rooms[roomPosition.x, roomPosition.y];
        Vector2Int distanceToStarterRoom = roomPosition - starterRoom;
        if (selectedRoom.connections[0])
        {
            int realX = (distanceToStarterRoom.x * roomSizeInTiles.x) + (distanceToStarterRoom.x * spaceBetweenRooms.x);
            int realY = (distanceToStarterRoom.y * roomSizeInTiles.y) + (distanceToStarterRoom.y * spaceBetweenRooms.y) + (tilemapEnd.y);
            wallsTilemap.SetTile(new Vector3Int(realX, realY, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX-1, realY, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX + 1, realY + 1, 0), wallTile);
            wallsTilemap.SetTile(new Vector3Int(realX - 2, realY + 1, 0), wallTile);
        }
        if (selectedRoom.connections[1])
        {
            int realX = (distanceToStarterRoom.x * roomSizeInTiles.x) + (distanceToStarterRoom.x * spaceBetweenRooms.x);
            int realY = (distanceToStarterRoom.y * roomSizeInTiles.y) + (distanceToStarterRoom.y * spaceBetweenRooms.y) + (tilemapStart.y);
            wallsTilemap.SetTile(new Vector3Int(realX, realY, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX-1, realY, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX + 1, realY - 1, 0), wallTile);
            wallsTilemap.SetTile(new Vector3Int(realX - 2, realY - 1, 0), wallTile);
        }
        if (selectedRoom.connections[2])
        {
            int realX = (distanceToStarterRoom.x * roomSizeInTiles.x) + (distanceToStarterRoom.x * spaceBetweenRooms.x) + (tilemapStart.x);
            int realY = (distanceToStarterRoom.y * roomSizeInTiles.y) + (distanceToStarterRoom.y * spaceBetweenRooms.y);
            wallsTilemap.SetTile(new Vector3Int(realX, realY, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX, realY-1, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX - 1, realY + 1, 0), wallTile);
            wallsTilemap.SetTile(new Vector3Int(realX - 1, realY - 2, 0), wallTile);
        }
        if (selectedRoom.connections[3])
        {
            int realX = (distanceToStarterRoom.x * roomSizeInTiles.x) + (distanceToStarterRoom.x * spaceBetweenRooms.x) + (tilemapEnd.x);
            int realY = (distanceToStarterRoom.y * roomSizeInTiles.y) + (distanceToStarterRoom.y * spaceBetweenRooms.y);
            wallsTilemap.SetTile(new Vector3Int(realX, realY, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX, realY - 1, 0), null);
            wallsTilemap.SetTile(new Vector3Int(realX + 1, realY + 1, 0), wallTile);
            wallsTilemap.SetTile(new Vector3Int(realX + 1, realY - 2, 0), wallTile);

        }
    }

    private void CreateRoomBorderTilemap(Vector2Int roomPosition)
    {
        for (int x = tilemapStart.x -1; x <= tilemapEnd.x +1; x++)
        {
            if ((x == tilemapStart.x -1 || x == tilemapEnd.x +1))
            {
                for (int y = tilemapStart.y -1; y <= tilemapEnd.y +1; y++)
                {
                    Vector2Int distanceToStarterRoom = roomPosition - starterRoom;
                    int realX = x + (distanceToStarterRoom.x * roomSizeInTiles.x) + (distanceToStarterRoom.x * spaceBetweenRooms.x);
                    int realY = y + (distanceToStarterRoom.y * roomSizeInTiles.y) + (distanceToStarterRoom.y * spaceBetweenRooms.y);
                    floorTilemap.SetTile(new Vector3Int(realX, realY, 0), floorTile);
                }
            }
        }

        for (int y = tilemapStart.y -1; y <= tilemapEnd.y +1; y++)
        {
            if ((y == tilemapStart.y -1 || y == tilemapEnd.y +1))
            {
                for (int x = tilemapStart.x -1; x <= tilemapEnd.x +1; x++)
                {
                    Vector2Int distanceToStarterRoom = roomPosition - starterRoom;
                    int realX = x + (distanceToStarterRoom.x * roomSizeInTiles.x) + (distanceToStarterRoom.x * spaceBetweenRooms.x);
                    int realY = y + (distanceToStarterRoom.y * roomSizeInTiles.y) + (distanceToStarterRoom.y * spaceBetweenRooms.y);
                    floorTilemap.SetTile(new Vector3Int(realX, realY, 0), floorTile);
                }
            }
        }
    }

    // Initial Room is supposed to spawn on 0,0. However, it is not on MazeRooms[0,0].
    private Vector3 RoomNumberToWorldPosition(Vector2Int roomPosition)
    { 
        Vector3 mainTransform = Camera.main.transform.position;
        Vector2Int distanceToStarterRoom = roomPosition - starterRoom;
        float x = (mainTransform.x + (roomSize.x * distanceToStarterRoom.x));
        float y = (mainTransform.y + (roomSize.y * distanceToStarterRoom.y));
        return new Vector3(x, y, 1);
    }

    private void UpdateActiveRoomTop()
    {
        activeRoom.ExitRoom();
        activeRoomPosition.y++;
        activeRoom = rooms[activeRoomPosition.x, activeRoomPosition.y];
        activeRoom.EnterRoom();
    }
    private void UpdateActiveRoomBottom()
    {
        activeRoom.ExitRoom();
        activeRoomPosition.y--;
        activeRoom = rooms[activeRoomPosition.x, activeRoomPosition.y];
        activeRoom.EnterRoom();
    }
    private void UpdateActiveRoomLeft()
    {
        activeRoom.ExitRoom();
        activeRoomPosition.x--;
        activeRoom = rooms[activeRoomPosition.x, activeRoomPosition.y];
        activeRoom.EnterRoom();
    }
    private void UpdateActiveRoomRight()
    {
        activeRoom.ExitRoom();
        activeRoomPosition.x++;
        activeRoom = rooms[activeRoomPosition.x, activeRoomPosition.y];
        activeRoom.EnterRoom();
    }

    void DebugPrintMaze()
    {
        string horizontalRow = null;
        string secondRow = null;
        for (int y = sizeY - 1; y >= 0; y--)
        {
            horizontalRow = null;
            secondRow = null; 
            for (int x = 0; x < sizeX; x++)
            {
                horizontalRow += "+";
                if (rooms[x, y].connections[3])
                {
                    horizontalRow += " ";
                }
                else { horizontalRow += "|"; }
                if (rooms[x, y].connections[1])
                {
                    secondRow += " ";
                }
                else { secondRow += "-"; }
            }
            Debug.Log(horizontalRow);
            Debug.Log(secondRow);
        }
    }
}
