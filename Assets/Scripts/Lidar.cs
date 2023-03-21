using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lidar : MonoBehaviour
{
    public float maxDistance = 5.0f;    

    public float distanceToReach = 0f;
    public float distanceToReachHelper = 0f;
    
    public RaycastHit hit;

    public bool DoHaveLeftWall(int posX, int posY)
    {
        transform.Rotate(0f, -90f, 0f, Space.Self);
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            transform.Rotate(0f, 90f, 0f, Space.Self);
            return true;
        }
        transform.Rotate(0f, 90f, 0f, Space.Self);
        return false;
    }

    public bool DoHaveUpWall(int posX, int posY)
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            return true;
        }
        return false;
    }

    public bool DoHaveRightWall(int posX, int posY)
    {
        transform.Rotate(0f, 90f, 0f, Space.Self);
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            transform.Rotate(0f, -90f, 0f, Space.Self);
            return true;
        }
        transform.Rotate(0f, -90f, 0f, Space.Self);
        return false;
    }

    public bool DoHaveDownWall(int posX, int posY)
    {
        transform.Rotate(0f, 180f, 0f, Space.Self);
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            transform.Rotate(0f, -180f, 0f, Space.Self);
            return true;
        }
        transform.Rotate(0f, -180f, 0f, Space.Self);
        return false;
    }

   // private bool isMoving;
   // private bool isUsingLidar;
   // private bool isRotating;
   // private float moveSpeed = 5.0f;

    /*
    void Start()
    {
        isMoving = false;
        isUsingLidar = false;
        isRotating = true;
    }

    void Update()
    {
        UseLidar();
        Rotating();
        Moving();
    }


    void UseLidar()
    {
        if (isUsingLidar)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
            {
                
            }
            
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
            {
                if (hit.distance < maxDistance)
                {
                    distanceToReach = maxDistance - hit.distance;
                    distanceToReachHelper = maxDistance - hit.distance;
                    isMoving = true;
                    isRotating = false;
                    isUsingLidar = false;
                    Debug.Log("Lidar 1 case is running");
                }
                else if (hit.distance >= maxDistance)
                {
                    isRotating = true;
                    isUsingLidar = false;
                    isMoving = false;
                    Debug.Log("Lidar 2 case is running");
                }
                else
                {
                    Debug.Log("Lidar says that collider is far more than maxDistance");
                    isMoving = false;
                    isRotating = false;
                    isUsingLidar = false;
                }
            }
            else
            {
                if(Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
                {
                    Debug.Log("New logic comes in");
                    isRotating = true;
                    isMoving = false;
                    isUsingLidar = false;
                }
                else
                {
                    distanceToReach = maxDistance;
                    distanceToReachHelper = maxDistance;
                    isMoving = true;
                    isUsingLidar = false;
                    isRotating = false;
                    Debug.Log("Lidar 3 case is running");
                }
            }
            
        }
    }

     void Rotating()
    {
        if (isRotating)
        {
            transform.Rotate(0f, -90f, 0f, Space.Self);
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
            {
                transform.Rotate(0f, 90f, 0f, Space.Self);
                if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
                {

                }
            }
            else
            {
                isMoving = true;
                isRotating = false;
                isUsingLidar = false;
                return;
            }

            /*
            bool isThereRightWall = false;
            bool isThereLeftWall = false;
            RaycastHit hit1;
            RaycastHit hit2;
           
           
            transform.Rotate(0f, 90f, 0f, Space.Self);
            if(Physics.Raycast(transform.position, transform.forward, out hit1, maxDistance))
            {
                isThereRightWall = true;
            }
            transform.Rotate(0f, -180f, 0f, Space.Self);
            if (Physics.Raycast(transform.position, transform.forward, out hit2, maxDistance))
            {
                isThereLeftWall = true;
            }
            transform.Rotate(0f, 90f, 0f, Space.Self);

            if (isThereLeftWall && isThereRightWall)
            {
                transform.Rotate(0f, 180f, 0f, Space.Self);
            }
            else if (isThereLeftWall)
            {
                transform.Rotate(0f, 90f, 0f, Space.Self);
            }
            else if (isThereRightWall)
            {
                transform.Rotate(0f, -90f, 0f, Space.Self);
            }
            else
                transform.Rotate(0f, 90f, 0f, Space.Self);
            
            isRotating = false;
            isUsingLidar = true;
            isMoving = false;
            Debug.Log("ROtating case is running");   
            
        }
    }

    void Moving()
    {
        if (isMoving)
        {
            transform.position = Vector3.forward + new Vector3(0f, 0f, 5f);
            isMoving = false;
            isUsingLidar = false;
            isRotating = true;
            return;
            /*
            if (distanceToReach == maxDistance)
            {
                if (distanceToReachHelper > 0)
                {
                    transform.position += transform.forward * Time.deltaTime * moveSpeed;
                    distanceToReachHelper -= Time.deltaTime * moveSpeed;
                    Debug.Log("Moving 1 case is running");

                    // Here can be condition to correct the movement if needed
                
                }
                else if (distanceToReachHelper < 0)
                {
                    distanceToReachHelper = 0f;
                    isUsingLidar = true;
                    isRotating = false;
                    isMoving = false;
                    Debug.Log("Moving 2 case is running");
                }
            }
            else if (distanceToReach < maxDistance)
            {
                if (distanceToReachHelper > 0)
                {
                    transform.position -= transform.forward * Time.deltaTime * moveSpeed;
                    distanceToReachHelper -= Time.deltaTime * moveSpeed;

                    Debug.Log("Moving 3 case is running");
                
                    // Same here

                }
                else if (distanceToReachHelper < 0)
                {
                    distanceToReachHelper = 0f;
                    isUsingLidar = true;
                    isRotating = false;
                    isMoving = false;

                    Debug.Log("Moving 4 case is running");
                }
            }
            else
            {
                Debug.Log("Distance to Reach is more than max distance");
                isMoving = false;
                isUsingLidar = false;
                isRotating = false;
            }
            
        }
    }
    */
}