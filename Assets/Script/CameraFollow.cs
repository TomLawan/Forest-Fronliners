using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // Player
    public Vector3 offset;            // Camera position offset
    public float smoothSpeed = 10f;   // Smoothness ng camera movement

    void LateUpdate()
    {
        // Ideal camera position
        Vector3 desiredPosition = target.position + offset;

        // Smooth follow
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.position = smoothedPosition;

        // Camera always looks at player
        transform.LookAt(target);
    }
}
