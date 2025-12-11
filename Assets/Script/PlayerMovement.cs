using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float rotationSpeed = 10f; // Bilis ng pag-ikot ng Player

    [Header("Component References")]
    public Transform cameraTransform; // I-drag ang CameraRig DITO
    private CharacterController controller;

    [Header("Gravity Settings")]
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    private float velocityY; // Ang y-axis velocity para sa gravity at jump

    void Start()
    {
        // Awtomatikong kunin ang CharacterController component
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            //Debug.LogError("ERROR: Kailangan ng CharacterController component!");
            enabled = false; // I-disable ang script kung walang controller
        }

        // Tiyakin na ang cameraTransform ay naka-assign
        if (cameraTransform == null)
        {
            //Debug.LogError("ERROR: Kailangan i-assign ang Camera Rig sa cameraTransform!");
            // Optional: Maaari mong hanapin ang Main Camera, pero mas maganda kung ang Rig ang i-assign.
        }
    }

    void Update()
    {
        // Siguraduhin na may controller at camera reference
        if (controller == null || cameraTransform == null) return;

        HandleMovementInput();
        ApplyGravity();
    }

    private void HandleMovementInput()
    {
        // Kunin ang raw input
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D o Left/Right
        float vertical = Input.GetAxisRaw("Vertical");    // W/S o Up/Down

        // Gawing Vector3 (movement direction sa local plane)
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            // 1. Calculate Target Angle (Relative to Camera)
            // Kinukuha ang anggulo ng input at idadagdag ang Y rotation ng Camera Rig
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            // 2. Smooth Turning
            // Ikinu-curve ang pag-ikot ng Player
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // 3. Calculate Final Movement Direction
            // Gagawin ang movement vector batay sa bagong rotation
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // 4. Move the Controller
            // Gumagalaw ang Player (pero walang Y movement, gravity ang bahala dun)
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }
    }

    private void ApplyGravity()
    {
        // Check kung nasa lupa
        if (controller.isGrounded)
        {
            // Kapag nasa lupa, zero ang negative velocity
            if (velocityY < 0)
            {
                velocityY = -2f; // Maliit na force pababa para maging sigurado na nakadikit sa lupa
            }

            // Jump Input (Optional)
            if (Input.GetButtonDown("Jump")) // Default key: Spacebar
            {
                // Simple Jump formula: v = sqrt(h * -2 * g)
                velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        // I-apply ang gravity (kahit tumatalon o bumabagsak)
        velocityY += gravity * Time.deltaTime;

        // I-move ang controller sa Y-axis (gravity)
        controller.Move(new Vector3(0, velocityY, 0) * Time.deltaTime);
    }
}