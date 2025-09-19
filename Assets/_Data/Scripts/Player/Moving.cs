using System;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float moveSpeed = 5f;

    private void Update()
    {
        this.Move();
    }

    void Move()
    {
        Vector3 direction2D = this.joystick.JoystickDirection;
        Vector3 direction3D = new Vector3(direction2D.x, 0, direction2D.y);

        this.rigidbody.linearVelocity = direction3D * this.moveSpeed;
    }
}