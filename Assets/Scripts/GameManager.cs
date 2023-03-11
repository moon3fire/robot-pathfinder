using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject wallGO;
    void Start()
    {
        /* TODO-FOR-GOR:: Haskanal kody incha anum
         for(int i = 0; i < 5; ++i)
        {
            Instantiate(wallGO, new Vector3(Random.Range(-3f, 5f), 0, Random.Range(-5f, 3f)), wallGO.transform.rotation);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
