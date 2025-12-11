using UnityEngine;

public class CameraRigRotation : MonoBehaviour
{
    public Transform target;        // Dito ilagay ang Player
    public float rotationSpeed = 3f;  // Bilis ng rotation

    void Update()
    {
        // 1. Gawing nakasunod ang Rig sa Player's position (Walang smoothing dito)
        // Hahawakan ng CameraFollow script ang smoothing.
        transform.position = target.position;

        // 2. CHECK: Kung naka-hold ang Right Mouse Button (RMB)
        if (Input.GetMouseButton(1))
        {
            // I-lock at itago ang cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Basahin ang Mouse X input (Kaliwa/Kanan)
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;

            // I-rotate ang CameraRig sa Y-axis (360 degrees)
            // Gamit ang Space.World, siguradong sa Y-axis ng mundo ang iikutan.
            transform.Rotate(Vector3.up, mouseX, Space.World);
        }
        else
        {
            // Kapag binitawan ang RMB, ibalik ang cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}