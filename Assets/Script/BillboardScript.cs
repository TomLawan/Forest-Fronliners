using System.Diagnostics;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        // Hanapin ang Main Camera sa simula
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
        else
        {
            
        }
    }

    // Gagamitin ang LateUpdate para masiguro na tapos na ang lahat ng movement calculations
    void LateUpdate()
    {
        if (mainCameraTransform == null) return;

        // I-rotate ang object para tingnan ang camera
        transform.LookAt(mainCameraTransform.position);

        // Ito ang crucial step para sa "Locking Y-axis" (Sprite lang ang iikot, hindi ang buong object)
        // Kukunin nito ang rotation na nilikha ng LookAt, pero ia-apply lang ang Y-axis rotation (rotation sa pagitan)
        transform.rotation = Quaternion.Euler(
            0f, // I-freeze ang X rotation (walang up/down tilt)
            transform.rotation.eulerAngles.y,
            0f  // I-freeze ang Z rotation (walang roll/tumble)
        );
    }
}