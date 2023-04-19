using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera menuCam;
    public CinemachineVirtualCamera newGameCam;
    public CinemachineVirtualCameraBase[] virtualCameras = new CinemachineVirtualCameraBase [3];
    private int currentCameraIndex;

    private void Start()
    {
        // Set the initial camera to the first virtual camera in the array
        currentCameraIndex = 0;
        virtualCameras[currentCameraIndex].gameObject.SetActive(true);
    }

    public void SwitchCamera(int cameraIndex)
    {
        // Disable the current camera
        virtualCameras[currentCameraIndex].gameObject.SetActive(false);

        // Set the new camera index
        currentCameraIndex = cameraIndex;

        // Enable the new camera
        virtualCameras[currentCameraIndex].gameObject.SetActive(true);
    }
}
