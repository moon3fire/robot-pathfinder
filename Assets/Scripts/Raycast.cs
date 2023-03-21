using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public RaycastHit test;
    public LayerMask objToHit = LayerMask.GetMask("BoxCollider");

    void Start()
    {
        // dist = test.distance;
         
        Vector3 a = new Vector3(1, 2, 3);
    }
 
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit test, objToHit))
        {
            Debug.Log("Ray hit collider in " + test.distance);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f, Color.red);
            
        }
        else 
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f, Color.blue);
            Debug.Log("Ray didn't hit collider");
        }
    }
}
