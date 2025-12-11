using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;           // Player (Kailangan pa rin para sa LookAt)
    public Vector3 offset = new Vector3(0, 3, -5); // Default offset
    public float smoothSpeed = 5f;     // Smoothness ng camera movement

    // Ang rotation ay gagawin ng CameraRig (parent)

    void LateUpdate()
    {
        // 1. Aayusin ang position ng camera para makuha ang desired offset
        // Ang desired position ay ang position ng parent (CameraRig) + ang Local offset
        Vector3 desiredPosition = transform.parent.position + transform.parent.TransformDirection(offset);

        // 2. Smooth follow (Lerp)
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.position = smoothedPosition;

        // 3. Camera always looks at player (LookAt)
        // Ito ang magbibigay ng vertical adjustment habang nagro-rotate
        transform.LookAt(target);
    }
}