using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraCustomController : MonoBehaviour
{
    public Joystick cameraJoystick;
    // Start is called before the first frame update
    void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetAxisCustom(string axisName)
    {
        if(axisName == "Mouse X")
        {
            return cameraJoystick.Horizontal;
        }
        else if (axisName == "Mouse Y")
        {
            return cameraJoystick.Vertical;
        }
        return UnityEngine.Input.GetAxis(axisName);
    }
}
