using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster
{
    public void Move(float _x,float _y,float _z)
    {
        float speed = Statdata.GetData(STAT_TYPE.SPEED);
        float fx = _x * speed * Time.deltaTime;
        float fy = _y * speed * Time.deltaTime;
        float fz = _z * speed * Time.deltaTime;

        transform.Translate(fx, fy, fz, Space.World);

        Vector3 dir = new Vector3(fx, fy, fz).normalized;
        transform.LookAt(transform.position + dir, Vector3.up);
    }

    public void Move(Vector3 dir)
    {

    }

    public void MoveToTarget(GameObject _target)
    {
        float speed = Statdata.GetData(STAT_TYPE.SPEED);
        Vector3 dir = _target.transform.position - transform.position;
        Vector3 MovePos = dir.normalized * speed * Time.deltaTime;

        transform.Translate(MovePos, Space.World);
        transform.LookAt(transform.position + dir, Vector3.up);
    }

    public void MoveToTarget()
    {
        float speed = Statdata.GetData(STAT_TYPE.SPEED);
        Vector3 dir = Target.transform.position - transform.position;
        Vector3 MovePos = dir.normalized * speed * Time.deltaTime;

        transform.Translate(MovePos, Space.World);
        transform.LookAt(transform.position + dir, Vector3.up);
    }
}