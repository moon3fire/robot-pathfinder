using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 1f;
    public float rotateSpeed = 1f;
    public float pitchSpeed = 1f;
    private float pitchAngle = 0f;
    private float radius;
    private bool isRotating = false;
    private bool isPitching = false;

    private Vector3 lastMousePosition;
    private Vector3 center;
    private Vector3 lastPitchMousePosition;
    private bool isCorrectedCoordinates;
    [SerializeField]
    private GameManager gameManager;

    private void Start()
    {
        isCorrectedCoordinates = false;
    }

    private void Update()
    {
        if(!isCorrectedCoordinates)
        {
            transform.position = new Vector3((gameManager.labyrinth.height - 1) * 10f, 25, 10f * gameManager.labyrinth.width / 2);
            center = gameManager.ground.GetComponent<MeshFilter>().sharedMesh.bounds.center;
            Debug.Log(center);
            radius = Mathf.Sqrt(transform.position.y * transform.position.y + transform.position.z * transform.position.z);
            isCorrectedCoordinates = true;
            return;
        }
        ZoomInOut();
        RotateCamera();
        MoveCameraUpDown();
        CorrectWithCircleCoordinates();
    }

    private void CorrectWithCircleCoordinates()
    {
        Vector3 newPosition = new Vector3(transform.position.x, 0f, transform.position.z).normalized * radius;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }

    private void ZoomInOut()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        transform.position += transform.forward * zoom;
    }

    private void RotateCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;
            float angle = deltaMousePosition.x * rotateSpeed / radius;
            transform.RotateAround(center, Vector3.up, angle);
            lastMousePosition = Input.mousePosition;
        }
    }

    private void MoveCameraUpDown()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isPitching = true;
            lastPitchMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isPitching = false;
        }

        if (isPitching)
        {
            Vector3 deltaMousePosition = Input.mousePosition - lastPitchMousePosition;
            pitchAngle -= deltaMousePosition.y * pitchSpeed;
            pitchAngle = Mathf.Clamp(pitchAngle, 0f, 85f);
            transform.localRotation = Quaternion.Euler(pitchAngle, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
            lastPitchMousePosition = Input.mousePosition;
        }
    }
}
