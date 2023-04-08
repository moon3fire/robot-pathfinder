using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    private bool isRotating;
    private bool isMoving;
    private bool isContinuingPath;

    public int robotPosX;
    public int robotPosZ;
    
    public int exitPosX;
    public int exitPosZ;
    
    
    //Assigned stuff

    [SerializeField]
    private Lidar lidar;
    [SerializeField]
    private LabyrinthGenerator labyrinth;
    [SerializeField]
    private GameManager gameManager;

    //Move speed to add at the end
    [SerializeField]
    private float moveSpeed = 5f;

    //directions enum
    enum DIRECTION : int {
        UP,
        RIGHT,
        DOWN,
        LEFT,
        EMPTY
    }

    //algorithm stuff

    Grid startingGrid;
    Grid solveGrid;
    List<Grid> open = new List<Grid>();
    List<Grid> closed = new List<Grid>();

    private void Start()
    {
        //allocating memory for collections

        //assigning starting positions
        robotPosX = gameManager.robotPosX;
        robotPosZ = gameManager.robotPosZ;

        //assigning solving positions
        exitPosX = labyrinth.solvePosX;
        exitPosZ = labyrinth.solvePosZ;

        startingGrid = new Grid(robotPosX, robotPosZ);
        startingGrid.hScore = ManhattanDistance(robotPosX, robotPosZ, exitPosX, exitPosZ);
        startingGrid.gScore = 0f;
        startingGrid.fScore = startingGrid.hScore + startingGrid.gScore;
        solveGrid = new Grid(exitPosX, exitPosZ);
        open.Add(startingGrid);
    }

    public static float ManhattanDistance(float startPosX, float startPosZ, float endPosX, float endPosZ)
    {
        return Math.Abs(startPosX - endPosX) + Math.Abs(startPosZ - endPosZ);
    }

    private void Update()
    {
        if (isRotating)
        {

        }
        if (isContinuingPath)
        {

        }
        if (isMoving)
        {

        }
    }

    void Search(Grid start)
    {
        start = DoGridChecks(start);
        if (start.doHaveLeftWall)
        {
            Grid leftGrid = new Grid(start.xPos - 5, start.zPos);
            leftGrid.hScore = ManhattanDistance(leftGrid.xPos, leftGrid.zPos, solveGrid.xPos, solveGrid.zPos);
            leftGrid.gScore = ManhattanDistance(startingGrid.xPos, startingGrid.zPos, leftGrid.xPos, leftGrid.zPos);
            leftGrid.fScore = leftGrid.gScore + leftGrid.hScore;
            open.Add(leftGrid);
        }
        if (start.doHaveUpWall)
        {
            Grid upGrid = new Grid(start.xPos, start.zPos + 5);
            upGrid.hScore = ManhattanDistance(upGrid.xPos, upGrid.zPos, solveGrid.xPos, solveGrid.zPos);
            upGrid.gScore = ManhattanDistance(startingGrid.xPos, startingGrid.zPos, upGrid.xPos, upGrid.zPos);
            upGrid.fScore = upGrid.gScore + upGrid.hScore;
            open.Add(upGrid);
        }
        if (start.doHaveRightWall)
        {
            Grid rightGrid = new Grid(start.xPos + 5, start.zPos);
            rightGrid.hScore = ManhattanDistance(rightGrid.xPos, rightGrid.zPos, solveGrid.xPos, solveGrid.zPos);
            rightGrid.gScore = ManhattanDistance(startingGrid.xPos, startingGrid.zPos, rightGrid.xPos, rightGrid.zPos);
            rightGrid.fScore = rightGrid.gScore + rightGrid.hScore;
            open.Add(rightGrid);
        }
        if (start.doHaveDownWall)
        {
            Grid downGrid = new Grid(start.xPos, start.zPos - 5);
            downGrid.hScore = ManhattanDistance(downGrid.xPos, downGrid.zPos, solveGrid.xPos, solveGrid.zPos);
            downGrid.gScore = ManhattanDistance(startingGrid.xPos, startingGrid.zPos, downGrid.xPos, downGrid.zPos);
            downGrid.fScore = downGrid.gScore + downGrid.hScore;
            open.Add(downGrid);
        }

        closed.Add(startingGrid);
        open = open.OrderBy(f => f.fScore).ThenBy(h => h.hScore).ToList<Grid>();
        Search(open[0]);
    }

    Grid DoGridChecks(Grid grid)
    {
        if (lidar.DoHaveLeftWall())
        {
            grid.doHaveLeftWall = false;
        }
        if (lidar.DoHaveRightWall())
            {
            grid.doHaveRightWall = false;
        }
        if (lidar.DoHaveUpWall())
        {
            grid.doHaveUpWall = false;
        }
        if (lidar.DoHaveDownWall())
        {
           grid.doHaveDownWall = false;
        }
        return grid;
    }

    
}
  /*  private void Start()
    {
        isRotating = true;
        isMoving = false;
        isLeftWallChecked = false;
        isRightWallChecked = false;
        isUpWallChecked = false;
        isDownWallChecked = false;

        pathStartStack = new Stack<(Grid, DIRECTION)>();

        //Debug.Log(labyrinth.height + labyrinth.width);
        matrix = new int[labyrinth.height, labyrinth.width];
        for (int i = 0; i < labyrinth.height; i++)
        {
            for (int j = 0; j < labyrinth.width; j++)
                matrix[i, j] = 0;
        }
        //reminder to add start word in game manager variables
        robotPosX = gameManager.robotPosX;
        robotPosZ = gameManager.robotPosY;
        matrix[robotPosX, robotPosZ] = 1;
        //works right
        //Debug.Log("robot start positions are... " + robotPosX + " " + robotPosY);
        exitPosX = labyrinth.solvePosX;
        exitPosZ = labyrinth.solvePosY;
        //works right
        //Debug.Log("exit positions are..." + exitPosX + " " + exitPosY);
    }

    private void Update()
    {
        if (isRotating)
        {
            Grid grid = new Grid(robotPosX, robotPosZ);
            if (!isLeftWallChecked)
            {
                if (lidar.DoHaveLeftWall(robotPosX, robotPosZ))
                {
                    isLeftWallChecked = true;
                    grid.doHaveLeftWall = false;
                    pathStartStack.Push((grid, DIRECTION.LEFT));
                    Debug.Log("There's left wall");
                }
            }
            if (!isRightWallChecked)
            {
                if (lidar.DoHaveRightWall(robotPosX, robotPosZ))
                {
                    isRightWallChecked = true;
                    grid.doHaveRightWall = false;
                    pathStartStack.Push((grid, DIRECTION.RIGHT));
                    Debug.Log("There's right wall");
                }
            }
            if (!isUpWallChecked)
            {
                if (lidar.DoHaveUpWall(robotPosX, robotPosZ))
                {
                    isUpWallChecked = true;
                    grid.doHaveUpWall = false;
                    pathStartStack.Push((grid, DIRECTION.UP));
                    Debug.Log("There's UP wall");
                }
            }
            if (!isDownWallChecked)
            {
                if (lidar.DoHaveDownWall(robotPosX, robotPosZ))
                {
                    isDownWallChecked = true;
                    grid.doHaveDownWall = false;
                    pathStartStack.Push((grid, DIRECTION.DOWN));
                    Debug.Log("There's down wall");
                }
            }
            isRotating = false;
            isMoving = true;
        }
        if (isMoving)
        {
            DIRECTION dir = DecideDirection(pathStartStack.Peek());
            pathStartStack.Pop();
            RotateByDirection(dir);
            transform.position += new Vector3(0f, 0f, 5f);
            robotPosX = (int)transform.position.x;
            robotPosZ = (int)transform.position.z;
            isMoving = false;
            isRotating = true;
        }
    }

    DIRECTION DecideDirection((Grid grid, DIRECTION direction) gridInfo)
    {
        if (!gridInfo.grid.doHaveLeftWall)
        {
            return DIRECTION.LEFT;
        }
        else if (!gridInfo.grid.doHaveUpWall)
        {
            return DIRECTION.UP;
        }
        else if (!gridInfo.grid.doHaveRightWall)
        {
            return DIRECTION.RIGHT;
        }
        else if (!gridInfo.grid.doHaveDownWall)
        {
            return DIRECTION.DOWN;
        }
        return DIRECTION.EMPTY;
    }

    void RotateByDirection(DIRECTION direction)
    {
        switch (direction)
        {
            case DIRECTION.LEFT:
                transform.Rotate(0f, -90f, 0f, Space.Self);
                break;
            case DIRECTION.UP:
                break;
            case DIRECTION.RIGHT:
                transform.Rotate(0f, 90f, 0f, Space.Self);
                break;
            case DIRECTION.DOWN:
                transform.Rotate(0f, 180f, 0f, Space.Self);
                break;
            case DIRECTION.EMPTY:
                break;
            default:
                break;
        }
    }

    Grid DoGridChecks(Grid grid)
    {
        if (isLeftWallChecked)
        {
            grid.doHaveLeftWall = false;
        }
        if (isRightWallChecked)
            {
            grid.doHaveRightWall = false;
        }
        if (isUpWallChecked)
        {
            grid.doHaveUpWall = false;
        }
        if (isDownWallChecked)
        {
           grid.doHaveDownWall = false;
        }
        return grid;
    }

}

*/