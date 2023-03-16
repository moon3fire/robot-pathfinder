using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class LabyrinthGenerator : MonoBehaviour 
{
    public int width = 10;
    public int height = 10;
    private float wallLength = 10f;

    private int spawnPosX;
    private int spawnPosY;

    [SerializeField]
    private List<GameObject> borders;
   
    public GameObject wall0Prefab;
    public GameObject wall90Prefab;

    // public GameObject[,] wallObjects;
    void Start()
    {
        borders = new List<GameObject>();
        GenerateNet();
    }

    void MakeNewPath()
    {
        spawnPosX = Random.Range(1, width);
        spawnPosY = Random.Range(1, height);

        //Will be continued
    }


    void GenerateNet()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject temp1 = Instantiate(wall0Prefab, new Vector3(i * wallLength + wallLength / 2f, 0, j * wallLength), Quaternion.identity);
                GameObject temp2 = Instantiate(wall90Prefab, new Vector3(i * wallLength, 0, j * wallLength + wallLength / 2f), Quaternion.Euler(0, 90, 0));
                temp1.name = "Wall_" + i + "_" + j;
                temp2.name = "Wall_" + j + "_" + i;
                if(i != 0 && j == 0 || i == 0 && j == 0)
                {
                    borders.Add(temp1);
                }
                if(j != 0 && i == 0 || j == 0 && i == 0)
                {
                    borders.Add(temp2);
                }
            }
        }

        for (int i = 0; i < width; i++)
        {
            GameObject temp = Instantiate(wall0Prefab, new Vector3((i + 0.5f) * wallLength, 0, (height - 0.5f) * wallLength + wallLength / 2f), Quaternion.identity);
            temp.name = "Wall_" + i + "_width_end";
            borders.Add(temp);
        }

        for(int i = 0; i < height; i++)
        {
            GameObject temp = Instantiate(wall90Prefab, new Vector3((width - 0.5f) * wallLength + wallLength / 2f, 0, (i + 0.5f) * wallLength), Quaternion.Euler(0, 90, 0));
            temp.name = "Wall_" + i + "_height_end";
            borders.Add(temp);
        }
    }

}