using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private void KeyboardMove()
    {
        if (Curr_State == STATE.ATTACK)
        {
            return;
        }

        float fx = Input.GetAxis("Horizontal");
        float fz = Input.GetAxis("Vertical");

        transform.Translate(fx * Speed * Time.deltaTime, 0.0f, fz * Speed * Time.deltaTime, Space.World);

        Vector3 dir = new Vector3(fx, 0.0f, fz).normalized;
        transform.LookAt(transform.position + dir, Vector3.up);

        if (fx == 0.0f && fz == 0.0f)
        {
            animator.SetInteger("Ani_State", (int)STATE.IDLE);
        }
        else
        {
            animator.SetInteger("Ani_State", (int)STATE.WALK);
        }
    }
    public void JoystickMove(Vector3 _dir, float _dist)
    {
        if (Curr_State == STATE.ATTACK)
        {
            return;
        }

        float DistSpeed = Speed * _dist;
        float fx = _dir.x * DistSpeed * Time.deltaTime;
        float fz = _dir.y * DistSpeed * Time.deltaTime;

        transform.Translate(fx, 0.0f, fz, Space.World);

        Vector3 dir = new Vector3(fx, 0.0f, fz).normalized;
        transform.LookAt(transform.position + dir, Vector3.up);

        PlayAni_float("Ani_Speed", DistSpeed);

        if (DistSpeed >= Speed * 0.6f)
        {
            ChangeState(STATE.RUN);
        }
        else if (DistSpeed > Speed * 0.4f)
        {
            ChangeState(STATE.WALK);
        }
        else
        {
            ChangeState(STATE.IDLE);
        }
    }
}