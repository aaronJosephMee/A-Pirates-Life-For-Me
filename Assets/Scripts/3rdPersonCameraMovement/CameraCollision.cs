using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraCollision : MonoBehaviour
{
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float smooth;
    private Vector3 _dollyDir;
    private float _distance;

    private void Awake()
    {
        _dollyDir = transform.localPosition.normalized;
        _distance = transform.localPosition.magnitude;
    }

    private void Update()
    {
        _distance = SetDistBetweenCamAndPlayer();
        transform.localPosition = Vector3.Lerp(transform.localPosition, _dollyDir * _distance, Time.deltaTime * smooth);
    }

    private float SetDistBetweenCamAndPlayer()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(_dollyDir * maxDistance);
        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out RaycastHit hit))
        {
            return Mathf.Clamp((hit.distance * 0.9f), minDistance, maxDistance);
        }

        return maxDistance;
    }
}
