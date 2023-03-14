using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class LabyrinthGenerator : MonoBehaviour 
{
    public int width = 10;
    public int height = 10;
    private float wallLength = 10f;

   
    public GameObject wall0Prefab;
    public GameObject wall90Prefab;

    // public GameObject[,] wallObjects;
    void Start()
    {
        GenerateNet();
    }

    void GenerateNet()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(wall0Prefab, new Vector3(i * wallLength + wallLength / 2f, 0, j * wallLength), Quaternion.identity);
            }
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(wall90Prefab, new Vector3(i * wallLength, 0, j * wallLength + wallLength / 2f), Quaternion.Euler(0, 90, 0));
            }
        }

        for (int i = 0; i < width; i++)
        {
            //NOTE:: THIS GONNA WORK ONLY WHEN WIDTH = HEIGHT :: NEED TO BE CHANGED LATER INTO 2 FOR'S
            Instantiate(wall0Prefab, new Vector3((i + 0.5f) * wallLength, 0, (height - 0.5f) * wallLength + wallLength / 2f), Quaternion.identity);
            Instantiate(wall90Prefab, new Vector3((width - 0.5f) * wallLength + wallLength / 2f, 0, (i + 0.5f) * wallLength), Quaternion.Euler(0, 90, 0));
        }
    }

}