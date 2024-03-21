using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCameraBase killCam;
    public Cinemachine.CinemachineVirtualCameraBase winCam;
    public void EnableKillCam()
    {
        killCam.Priority = 20;
    }

    public void EnableWinCam()
    {
        winCam.Priority = 20;
    }
}
