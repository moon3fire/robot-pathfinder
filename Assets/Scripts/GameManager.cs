using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int robotPosX;
    public int robotPosY;

    [SerializeField]
    public LabyrinthGenerator labyrinth;
    
    public GameObject ground;

    [SerializeField]
    private GameObject[] cells;

    public GameObject robot;

    [SerializeField]
    Material groundColor;

    void Awake()
    {
        ground.transform.position = new Vector3(10 * (labyrinth.height - 1) / 2, -1, 10 * (labyrinth.width - 1) / 2);
        ground.GetComponent<BoxCollider>().size = new Vector3(10 * labyrinth.height, 0, 10 * labyrinth.width);
        ground.GetComponent<Transform>().localScale = new Vector3(10 * labyrinth.height, 0, 10 * labyrinth.width);
        Material unlitMaterial = new Material(Shader.Find("Unlit/Color"));
        unlitMaterial.color = groundColor.color;
        ground.GetComponent<MeshRenderer>().material = unlitMaterial;
    }

    void Start()
    {
        cells = GameObject.FindGameObjectsWithTag("Respawn");
        foreach(GameObject cell in cells)
        {
            cell.SetActive(false);
        }
        robotPosY = (int)Random.Range(0, labyrinth.width);
        robotPosX = (int)Random.Range(0, labyrinth.height);
        robot.transform.position = new Vector3(10f * robotPosX, -0.5f, 10f * robotPosY);
    }

    void Update()
    {
        
    }
}
