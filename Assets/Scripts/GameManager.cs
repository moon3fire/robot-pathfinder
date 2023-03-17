using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public LabyrinthGenerator labyrinth;
    
    public GameObject ground;
    [SerializeField]
    Material groundColor;
    void Awake()
    {
        ground.transform.position = new Vector3(10 * (labyrinth.height - 1) / 2, 0, 10 * (labyrinth.width - 1) / 2);
        ground.GetComponent<BoxCollider>().size = new Vector3(10 * labyrinth.height, 0, 10 * labyrinth.width);
        ground.GetComponent<Transform>().localScale = new Vector3(10 * labyrinth.height, 0, 10 * labyrinth.width);
        Material unlitMaterial = new Material(Shader.Find("Unlit/Color"));
        unlitMaterial.color = groundColor.color;
        ground.GetComponent<MeshRenderer>().material = unlitMaterial;
    }

    void Start()
    {
    }

    void Update()
    {
        
    }
}
