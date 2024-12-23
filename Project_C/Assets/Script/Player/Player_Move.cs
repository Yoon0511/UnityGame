using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    bool isjoystick_drag;
    private void KeyboardMove()
    {
        if (curr_state == STATE.ATTACK)
        {
            return;
        }

        float fx = Input.GetAxis("Horizontal");
        float fz = Input.GetAxis("Vertical");

        transform.Translate(fx * speed * Time.deltaTime, 0.0f, fz * speed * Time.deltaTime, Space.World);

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
    public void JoystickMove(Vector3 _dir, float _dist,bool _isdrag)
    {
        if (curr_state == STATE.ATTACK)
        {
            return;
        }
        isjoystick_drag = _isdrag;
        float DistSpeed = speed * _dist;
        float fx = _dir.x * DistSpeed * Time.deltaTime;
        float fz = _dir.y * DistSpeed * Time.deltaTime;

        transform.Translate(fx, 0.0f, fz, Space.World);

        Vector3 dir = new Vector3(fx, 0.0f, fz).normalized;
        transform.LookAt(transform.position + dir, Vector3.up);

        StateChageJoystick(DistSpeed);
    }
}