using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoomCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomedOutFOV = 60.0f;
    public float zoomedInFOV = 30.0f;
    public float zoomSpeed = 10.0f;

    private void Update()
    {
        float targetFOV = Input.GetMouseButton(1) ? zoomedInFOV : zoomedOutFOV;
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }
}

