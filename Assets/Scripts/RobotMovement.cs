using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{

    private bool isLeftWallChecked;
    private bool isRightWallChecked;
    private bool isUpWallChecked;
    private bool isDownWallChecked;


    private bool isRotating;
    private bool isMoving;

    public int robotPosX;
    public int robotPosY;
    
    public int exitPosX;
    public int exitPosY;
    
    [SerializeField]
    private Lidar lidar;
    [SerializeField]
    private LabyrinthGenerator labyrinth;
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private float moveSpeed = 5f;

    Stack<Grid> stack;
    List<Grid> paths;

    [SerializeField]
    private int[,] matrix;

    private void Start()
    {
        isRotating = true;
        isMoving = false;
        isLeftWallChecked = false;
        isRightWallChecked = false;
        isUpWallChecked = false;
        isDownWallChecked = false;

        //Debug.Log(labyrinth.height + labyrinth.width);
        matrix = new int[labyrinth.height, labyrinth.width];
        for (int i = 0; i < labyrinth.height; i++)
        {
            for (int j = 0; j < labyrinth.width; j++)
                matrix[i, j] = 0;
        }
        //reminder to add start word in game manager variables
        robotPosX = gameManager.robotPosX;
        robotPosY = gameManager.robotPosY;
        matrix[robotPosX, robotPosY] = 1;
        //works right
        //Debug.Log("robot start positions are... " + robotPosX + " " + robotPosY);
        exitPosX = labyrinth.solvePosX;
        exitPosY = labyrinth.solvePosY;
        //works right
        //Debug.Log("exit positions are..." + exitPosX + " " + exitPosY);
    }

    private void Update()
    {
        if (isRotating)
        {
            if (!isLeftWallChecked)
            {
                if (lidar.DoHaveLeftWall(robotPosX, robotPosY))
                {
                    isLeftWallChecked = true;
                    Debug.Log("There's left wall");
                }
            }
            if (!isRightWallChecked)
            {
                if (lidar.DoHaveRightWall(robotPosX, robotPosY))
                {
                    isRightWallChecked = true;
                    Debug.Log("There's right wall");
                }
            }
            if (!isUpWallChecked)
            {
                if (lidar.DoHaveUpWall(robotPosX, robotPosY))
                {
                    isUpWallChecked = true;
                    Debug.Log("There's UP wall");
                }
            }
            if (!isDownWallChecked)
            {
                if (lidar.DoHaveDownWall(robotPosX, robotPosY))
                {
                    isDownWallChecked = true;
                    Debug.Log("There's down wall");
                }
            }
            isRotating = false;
            isMoving = true;
        }
        if (isMoving)
        {

        }
    }
}
