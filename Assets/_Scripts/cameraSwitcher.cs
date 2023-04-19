using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class cameraSwitcher : MonoBehaviour
{
    
    public  CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera menuCam;
   
    
    public void Start()
    {
        menuCam = currentCamera;
        currentCamera.Priority++;
    }

    public void UpdateCamera(CinemachineVirtualCamera target)
    {
        
        currentCamera.Priority--;

        currentCamera = target;

        currentCamera.Priority++;
        Debug.Log("Button clicked");

        if (Input.GetKeyDown(KeyCode.Escape)){
           
        }
    
    }
}