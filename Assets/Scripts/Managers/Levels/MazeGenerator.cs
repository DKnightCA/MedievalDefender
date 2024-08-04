using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public int sizeX;
    public int sizeY;
    public Vector2Int starterRoom;
    public Vector2Int finalRoom;

    public GameObject roomLimitPrefab;
    public GameObject roomPrefab;
    public MazeRoom[,] rooms;
    public MazeRoom activeRoom;
    public Vector2 roomSize;

    private void Awake()
    {
        
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
        activeRoom = rooms[starterRoom.x, starterRoom.y];
    }

    void InitializeRooms()
    {
        rooms = new MazeRoom[sizeX, sizeY];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                GameObject newRoom = Instantiate(roomPrefab, RoomNumberToPosition(new Vector2Int(x, y)), Quaternion.identity);
                newRoom.name = $"MazeRoom_{x}_{y}";
                //Debug.Log("POSITION: " + RoomNumberToPosition(new Vector2Int(x, y)).x + RoomNumberToPosition(new Vector2Int(x, y)).y);
                rooms[x, y] = newRoom.GetComponent<MazeRoom>();
            }
        }
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
                Vector2Int chosenNeighbor = neighbors[Random.Range(0, neighbors.Count)];
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
                    Vector3 position = RoomNumberToPosition(new Vector2Int(x, y));
                    position.y += roomSize.y / 2;
                    limit = Instantiate(roomLimitPrefab, position, Quaternion.identity);
                    limit.transform.Find("TopLimit").gameObject.SetActive(true);
                    limit.transform.Find("BottomLimit").gameObject.SetActive(true);
                    limit.name = $"RoomLimit_{x}_{y}_N";

                }
                if (rooms[x, y].connections[3])
                {
                    Vector3 position = RoomNumberToPosition(new Vector2Int(x, y));
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
            Vector2Int chosenNeighbor = neighbors[Random.Range(0, neighbors.Count)];
            CreateConnection(endRoomPos, chosenNeighbor);
            visited.Add(endRoomPos);
        }
    }


    // Initial Room is supposed to spawn on 0,0. However, it is not on MazeRooms[0,0].
    private Vector3 RoomNumberToPosition(Vector2Int roomPosition)
    { 
        Vector3 mainTransform = Camera.main.transform.position;
        Vector2Int distanceToStarterRoom = roomPosition - starterRoom;
        float x = (mainTransform.x + (roomSize.x * distanceToStarterRoom.x));
        float y = (mainTransform.y + (roomSize.y * distanceToStarterRoom.y));
        return new Vector3(x, y, 1);
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
