using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster
{
    public void Move(float _x,float _y,float _z)
    {
        float fx = _x * Speed * Time.deltaTime;
        float fy = _y * Speed * Time.deltaTime;
        float fz = _z * Speed * Time.deltaTime;

        transform.Translate(fx, fy, fz, Space.World);

        Vector3 dir = new Vector3(fx, fy, fz).normalized;
        transform.LookAt(transform.position + dir, Vector3.up);
    }

    public void Move(Vector3 dir)
    {

    }

    public void MoveToTarget(GameObject _target)
    {
        Vector3 dir = _target.transform.position - transform.position;
        Vector3 MovePos = dir.normalized * Speed * Time.deltaTime;

        transform.Translate(MovePos, Space.World);
        transform.LookAt(transform.position + dir, Vector3.up);
    }

    public void MoveToTarget()
    {
        Vector3 dir = Target.transform.position - transform.position;
        Vector3 MovePos = dir.normalized * Speed * Time.deltaTime;

        transform.Translate(MovePos, Space.World);
        transform.LookAt(transform.position + dir, Vector3.up);
    }
}