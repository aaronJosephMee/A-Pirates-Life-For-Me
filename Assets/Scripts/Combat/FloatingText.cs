using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 2f;
    public Vector3 Offset = new Vector3(0, 2, 0);
    public Vector3 randIntensity = new Vector3(0.5f, 0.0f, 0.0f);

    public Camera playerCamera;   

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyTime);
        transform.localPosition += Offset;
        transform.localPosition += new Vector3(Random.Range(-randIntensity.x, randIntensity.x), Random.Range(-randIntensity.y, randIntensity.y), Random.Range(-randIntensity.z, randIntensity.z));
        playerCamera = Camera.main; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward,
            playerCamera.transform.rotation * Vector3.up);
    }
}
