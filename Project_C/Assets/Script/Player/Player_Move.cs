using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public partial class Player
{
    bool isjoystick_drag;

    public void JoystickMove(Vector3 _dir, float _dist,bool _isdrag)
    {
        if (!PV.IsMine) return;

        if (CurrState == (int)STATE.ATTACK ||
            IsStun == true || IsGuard == true)
        {
            return;
        }
        isjoystick_drag = _isdrag;
        float speed = Statdata.GetData(STAT_TYPE.SPEED);
        float DistSpeed = speed * _dist;

        // 카메라 기준 방향 변환
        Transform cam = Camera.main.transform;
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // 입력 방향(_dir)을 카메라 기준으로 변환
        Vector3 moveDir = camForward * _dir.y + camRight * _dir.x;
        moveDir.Normalize();

        // 이동
        transform.Translate(moveDir * DistSpeed * Time.deltaTime, Space.World);

        // 회전
        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        //float fx = _dir.x * DistSpeed * Time.deltaTime;
        //float fz = _dir.y * DistSpeed * Time.deltaTime;
        //
        //
        //transform.Translate(fx, 0.0f, fz, Space.World);
        //Vector3 dir = new Vector3(fx, 0.0f, fz).normalized;
        //transform.LookAt(transform.position + dir, Vector3.up);

        StateChageJoystick(DistSpeed);
    }
}