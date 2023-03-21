using System.Collections.Generic;
using UnityEngine;
using Cell = System.ValueTuple<int, int>;

public class LabyrinthGenerator : MonoBehaviour 
{
    public int solvePosX;
    public int solvePosY;
    public int width = 10;
    public int height = 10;
    private float wallLength = 10f;

    public GameObject wall0Prefab;
    public GameObject wall90Prefab;
    public GameObject emptySpacePrefab;

    public int[,] matrix;

    enum Walls : int {
        UP_WALL = 1,
        RIGHT_WALL = 2,
        DOWN_WALL = 4,
        LEFT_WALL = 8
    }

    enum Neighbour : int {
        UP,
        RIGHT,
        DOWN,
        LEFT,
        UNKNOWN
    }

    void Awake()
    {
        matrix = new int[height, width];
        InitializeMatrix();
        DFS();
    }

    void Start()
    {
        OpenExit();
        PaintNet();
    }

    void OpenExit()
    {
        //Debug.Log("width " + width);
        solvePosX = 0;
        solvePosY = UnityEngine.Random.Range(0, width);
        DestroyWall((0, solvePosX), Walls.UP_WALL);
    }

    void InitializeMatrix()
    {
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
                matrix[i, j] = 15;
        }
    }

    void DFS()
    {
        Stack<Cell> stack = new Stack<Cell>();
        bool[,] isVisited = new bool[height, width];

        int x = UnityEngine.Random.Range(0, height);
        int y = UnityEngine.Random.Range(0, width);

        Cell currentCell = (x, y);
        isVisited[x, y] = true;
        stack.Push(currentCell);

        while(stack.Count > 0)
        {
            currentCell = stack.Peek();
            stack.Pop();
            Neighbour currentNeighbour = GetRandomNeighbour(currentCell, isVisited);
            if (currentNeighbour != Neighbour.UNKNOWN)
            {
                stack.Push(currentCell);
                Cell currentNeighbourCell = GetNeighbourCell(currentCell, currentNeighbour);
                isVisited[currentNeighbourCell.Item1, currentNeighbourCell.Item2] = true;
                switch (currentNeighbour)
                {
                    case Neighbour.UP:
                        DestroyWall(currentCell, Walls.UP_WALL);
                        break;
                    case Neighbour.LEFT:
                        DestroyWall(currentCell, Walls.LEFT_WALL);
                        break;
                    case Neighbour.RIGHT:
                        DestroyWall(currentCell, Walls.RIGHT_WALL);
                        break;
                    case Neighbour.DOWN:
                        DestroyWall(currentCell, Walls.DOWN_WALL);
                        break;
                    case Neighbour.UNKNOWN:
                        Debug.Log("ERROR: UNKNOWN NEIGHBOUR");
                        break;
                    default:
                        break;
                }
                stack.Push(currentNeighbourCell);
            }
        }
    }

    Cell GetNeighbourCell(Cell cell, Neighbour neighbour)
    {
        switch (neighbour)
        {
            case Neighbour.DOWN:
                return (cell.Item1 + 1, cell.Item2);
            case Neighbour.UP:
                return (cell.Item1 - 1, cell.Item2);
            case Neighbour.LEFT:
                return (cell.Item1, cell.Item2 - 1);
            case Neighbour.RIGHT:
                return (cell.Item1, cell.Item2 + 1);
            case Neighbour.UNKNOWN:
                return (-1, -1);
            default:
                return (-1, -1);
        }
    }

    Neighbour GetRandomNeighbour(Cell cell, bool[,] isVisited)
    {
        List<Neighbour> neighbourList = new List<Neighbour>();
        if (cell.Item1 > 0)
        {
            if (!isVisited[cell.Item1 - 1, cell.Item2])
            {
                neighbourList.Add(Neighbour.UP);
            }
        }
        if (cell.Item1 < height - 1)
        {
            if (!isVisited[cell.Item1 + 1, cell.Item2])
            {
                neighbourList.Add(Neighbour.DOWN);
            }
        }
        if (cell.Item2 > 0)
        {
            if (!isVisited[cell.Item1, cell.Item2 - 1])
            {
                neighbourList.Add(Neighbour.LEFT);
            }
        }
        if (cell.Item2 < width - 1)
        {
            if (!isVisited[cell.Item1, cell.Item2 + 1])
            {
                neighbourList.Add(Neighbour.RIGHT);
            }
        }
        if (neighbourList.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, neighbourList.Count);
            return neighbourList[rand];
        }
        return Neighbour.UNKNOWN;
    }


    private void DestroyWall(Cell cell, Walls wall)
    {
        matrix[cell.Item1, cell.Item2] ^= (int)wall;

        switch (wall)
        {
            case Walls.UP_WALL:
                if (cell.Item1 > 0)
                    matrix[cell.Item1 - 1, cell.Item2] ^= (int)Walls.DOWN_WALL;
                break;
            case Walls.RIGHT_WALL:
                if (cell.Item2 < width - 1)
                    matrix[cell.Item1, cell.Item2 + 1] ^= (int)Walls.LEFT_WALL;
                break;
            case Walls.DOWN_WALL:
            if (cell.Item1 < height - 1)
                    matrix[cell.Item1 + 1, cell.Item2] ^= (int)Walls.UP_WALL;
                break;
            case Walls.LEFT_WALL:
            if (cell.Item2 > 0)
                    matrix[cell.Item1, cell.Item2 - 1] ^= (int)Walls.RIGHT_WALL;
                break;
            default:
                break;
        }
    }

    bool DoHaveWall(Cell cell, Walls wall)
    {
        return (matrix[cell.Item1, cell.Item2] & (int)wall) != 0;
    }

    void PaintNet()
    {
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                GameObject cell = Instantiate(emptySpacePrefab, new Vector3(i * wallLength, 0, j * wallLength), Quaternion.identity);
                cell.name = "EmptySpace_" + i + "_" + j;
                if (DoHaveWall((i, j), Walls.LEFT_WALL))
                {
                    GameObject leftWall = Instantiate(wall0Prefab, new Vector3(i * wallLength, 0, j * wallLength - wallLength * 0.5f), Quaternion.identity);
                }
                if (DoHaveWall((i, j), Walls.DOWN_WALL))
                {
                    GameObject downWall = Instantiate(wall90Prefab, new Vector3(i * wallLength + 0.5f * wallLength, 0, j * wallLength), Quaternion.Euler(0, 90, 0));
                }
                if (DoHaveWall((i, j), Walls.RIGHT_WALL))
                {
                    GameObject rightWall = Instantiate(wall0Prefab, new Vector3(i * wallLength, 0, j * wallLength + 0.5f * wallLength), Quaternion.identity);
                }
                if (DoHaveWall((i, j), Walls.UP_WALL))
                {
                    GameObject upWall = Instantiate(wall90Prefab, new Vector3(i * wallLength + 0.5f * wallLength - wallLength, 0, j * wallLength), Quaternion.Euler(0, 90, 0));
                }
            }
        }
    }
}