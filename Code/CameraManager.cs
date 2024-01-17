using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCameraBase cameraBase;

    public void KillCamOn()
    {
        cameraBase.Priority = 20;
    }
}
