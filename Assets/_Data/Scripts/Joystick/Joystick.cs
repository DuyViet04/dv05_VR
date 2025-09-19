using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Joystick : MonoBehaviour
{
    [SerializeField] private Transform joystickBackground;
    [SerializeField] private Transform joystickHandle;

    private Controls controls;
    private Vector3 touchPosition;
    private Vector3 basePosition;
    private Vector3 joystickDirection;
    private float radius;
    private bool isTouch = false;

    private void Awake()
    {
        this.controls = new Controls();
        this.controls.UI.TouchPress.started += this.StartTouch;
        this.controls.UI.Touch.performed += this.PerformTouch;
        this.controls.UI.TouchPress.canceled += this.CancelTouch;

        this.basePosition = this.joystickBackground.position;
        this.radius = this.joystickBackground.GetComponent<RectTransform>().sizeDelta.y / 2;
    }

    private void OnEnable()
    {
        this.controls.Enable();
    }

    private void OnDisable()
    {
        this.controls.Disable();
    }

    void StartTouch(InputAction.CallbackContext ctx)
    {
        this.isTouch = true;
        this.touchPosition = this.controls.UI.Touch.ReadValue<Vector2>();

        if (this.touchPosition.x > Screen.width / 2) return;
        this.joystickBackground.position = this.touchPosition;
        this.joystickHandle.position = this.touchPosition;
    }

    void PerformTouch(InputAction.CallbackContext ctx)
    {
        if (!this.isTouch) return;
        this.touchPosition = ctx.ReadValue<Vector2>();
        this.joystickHandle.position = this.touchPosition;

        this.joystickDirection = (this.joystickHandle.position - this.joystickBackground.position).normalized;
        float distance = Vector3.Distance(this.joystickBackground.position, this.joystickHandle.position);
        if (distance > this.radius)
        {
            this.joystickHandle.position = this.joystickBackground.position + this.joystickDirection * this.radius;
        }
        else
        {
            this.joystickHandle.position = this.touchPosition;
        }
    }

    void CancelTouch(InputAction.CallbackContext ctx)
    {
        this.isTouch = false;
        this.joystickBackground.position = this.basePosition;
        this.joystickHandle.position = this.basePosition;
    }
}