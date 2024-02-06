using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowCameraTarget : MonoBehaviour
{
    [SerializeField] private Transform camTarget;
    [SerializeField] private float positionLerpRate;
    [SerializeField] private float rotationLerpRate;

    private void Start()
    {
        transform.position = camTarget.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, camTarget.position, positionLerpRate);
        transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.rotation, rotationLerpRate);
    }
}
