using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.5f;
    [SerializeField]
    private float xPlayerInput = 0;
    [SerializeField]
    private float yPlayerInput = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
       xPlayerInput = Input.GetAxis("Horizontal");
       yPlayerInput = Input.GetAxis("Vertical");

       gameObject.transform.position +=
       new Vector3( moveSpeed * Time.deltaTime * xPlayerInput,
                    0,
                    moveSpeed * Time.deltaTime * yPlayerInput);
    }
}
