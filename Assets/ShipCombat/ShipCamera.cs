using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCamera : MonoBehaviour
{
    public Transform Ship;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = Ship.position + offset;
    }
}
