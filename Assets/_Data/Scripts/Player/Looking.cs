using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Looking : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    
    private Quaternion baseRotation;
    private Quaternion offset;

    private void OnEnable()
    {
        if (AttitudeSensor.current == null) return;
        InputSystem.EnableDevice(AttitudeSensor.current);
    }

    private void Update()
    {
        this.Look();
    }

    void Look()
    {
        if (AttitudeSensor.current == null) return;
        Quaternion deviceRotation = AttitudeSensor.current.attitude.ReadValue();

        Quaternion newRotation =
            new Quaternion(-deviceRotation.x, -deviceRotation.y, deviceRotation.z, deviceRotation.w);
        this.mainCamera.transform.rotation = newRotation;
    }
}