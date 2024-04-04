using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapCollision : MonoBehaviour
{

    private GameObject spawnedObject;

    public GameObject objectToSpawn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (spawnedObject == null)
            {
                Vector3 spawnPosition = new Vector3(transform.position.x, -7.4f, transform.position.z);
                spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(spawnedObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            OverworldMapManager.Instance.TransitionBackToMap();
        }
        
    }
}