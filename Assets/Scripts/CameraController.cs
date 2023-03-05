using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject robot;
    private Vector3 offset;
    void Start()
    {
        offset = new Vector3(0, 10, -4);
    }

    
    void Update()
    {
        gameObject.transform.position = robot.transform.position + offset;
    }
}
