using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;

public partial class MainCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject CAMERAMOVE;

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

    [SerializeField]
    float ZoomSpeed;

    [SerializeField]
    Camera Camera;
    private void Start()
    {
        Shared.MainCamera = this;
        Vector3 angles = CAMERAMOVE.transform.eulerAngles;
        Yaw = angles.y;
        Pitch = angles.x;
        //Target = Shared.GameMgr.PLAYEROBJ.transform;
        //
        //if (Target == null)
        //{
        //    Debug.LogError("Target이 설정되지 않았습니다!");
        //}
    }

    private void Update()
    {
        if (CameraShake)
        {
            return;
        }
        if(Shared.UiMgr.GetIsOpenUi())
        {
            return;
        }    

        if (Input.GetMouseButton(0) && Shared.GameMgr.IsJoystickDrag() == false)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Yaw += mouseX * MouseSensitivity;
            //Yaw = Mathf.Clamp(Yaw, MinYaw, MaxYaw);

            Pitch -= mouseY * MouseSensitivity;
            Pitch = Mathf.Clamp(Pitch, MinPitch, MaxPitch);
        }
    }

    private void LateUpdate()
    {
        if (CameraShake) return;
        if (Target == null) return;

        //마우스 휠 - 카메라 줌인/줌아웃
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Offset -= Offset.normalized * scroll * ZoomSpeed;

        // 최소/최대 줌 거리 제한
        float distance = Offset.magnitude;
        distance = Mathf.Clamp(distance, 5f, 20f);
        Offset = Offset.normalized * distance;

        //메인 카메라
        Quaternion rotation = Quaternion.Euler(Pitch, Yaw, 0);
        Vector3 desiredPosition = Target.position + rotation * Offset;
       
        CAMERAMOVE.transform.localPosition = desiredPosition;
        CAMERAMOVE.transform.LookAt(Target);

        //Quaternion targetRotation = Quaternion.Euler(0, Yaw, 0);
        //Target.rotation = Quaternion.Slerp(Target.rotation, targetRotation, Time.deltaTime * 30f);
    }

    public void SetTarget(Transform _tartget)
    {
        Target = _tartget;
    }

    public void SaveCameraOption()
    {

    }

    public void ReturnCameraOption()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        Camera.fieldOfView = 60f;
    }
}
