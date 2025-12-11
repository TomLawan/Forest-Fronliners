using UnityEngine;
using Cinemachine;

public class HoldRightMouseToLook : MonoBehaviour
{
    public CinemachineFreeLook freeLook;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            freeLook.m_XAxis.m_MaxSpeed = 300;
            freeLook.m_YAxis.m_MaxSpeed = 2;
        }
        else
        {
            freeLook.m_XAxis.m_MaxSpeed = 0;
            freeLook.m_YAxis.m_MaxSpeed = 0;
        }
    }
}
