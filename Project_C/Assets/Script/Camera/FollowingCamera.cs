using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField]
    private Transform Target;

    [SerializeField]
    private Vector3 Offset = new Vector3(0, 2, -5);

    [SerializeField]
    private float MouseSensitivity = 3f;

    private float Yaw;
    private float Pitch; 

    [SerializeField]
    private float MinPitch = -30f;
    [SerializeField]
    private float MaxPitch = 60f;
    [SerializeField]
    private float MinYaw = -30f;
    [SerializeField]
    private float MaxYaw = 60f;

    private void Start()
    {
        Vector3 angles = transform.eulerAngles;
        Yaw = angles.y;
        Pitch = angles.x;

        if (Target == null)
        {
            Debug.LogError("Target이 설정되지 않았습니다!");
        }
    }

    private void Update()
    {
        if (Shared.MainCamera.CameraShake) return;
        
        if (Input.GetMouseButton(0) && Shared.GameMgr.IsJoystickDrag() == false)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Yaw += mouseX * MouseSensitivity;
            Yaw = Mathf.Clamp(Yaw, MinYaw, MaxYaw);

            Pitch -= mouseY * MouseSensitivity;
            Pitch = Mathf.Clamp(Pitch, MinPitch, MaxPitch);
        }
    }

    private void LateUpdate()
    {
        if (Target == null) return;

        Quaternion rotation = Quaternion.Euler(Pitch, Yaw, 0);
        Vector3 desiredPosition = Target.position + rotation * Offset;

        transform.position = desiredPosition;
        
        if (Shared.MainCamera.CameraShake) return;
        transform.LookAt(Target);
    }

    IEnumerator ShakeCoroutine()
    {
        yield return null;
    }
}
