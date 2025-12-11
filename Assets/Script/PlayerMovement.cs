using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float rotationSpeed = 10f;
    public Transform cameraTransform;
    public CharacterController controller;

    private float gravity = -9.81f;
    private float velocityY;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");   // A / D
        float vertical = Input.GetAxis("Vertical");       // W / S

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Direction relative to Camera
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            // Smooth turning
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        // Gravity
        if (controller.isGrounded && velocityY < 0)
        {
            velocityY = -2f;
        }
        velocityY += gravity * Time.deltaTime;

        controller.Move(new Vector3(0, velocityY, 0) * Time.deltaTime);
    }
}
