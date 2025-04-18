using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BattleMgr : MonoBehaviour
{
    private void Awake()
    {
        Shared.BattleMgr = this;
    }

    public bool Attack(Transform _p1, Transform _p2)
    {
        Vector3 min = Vector3.Min(_p1.position, _p2.position);
        Vector3 max = Vector3.Max(_p1.position, _p2.position);

        //foreach (var monster in monsters)
        //{
        //    Vector3 pos = monster.position;
        //
        //    if (pos.x >= min.x && pos.x <= max.x &&
        //        pos.y >= min.y && pos.y <= max.y &&
        //        pos.z >= min.z && pos.z <= max.z)
        //    {
        //        Debug.Log("몬스터 감지됨: " + monster.name);
        //        // 여기에 데미지 처리 등 넣기
        //    }
        //}
        return true;
    }
    public bool Attack(Character _target,Transform _p1, Transform _p2, Transform _p3)
    {
        // Barycentric 좌표 계산
        Vector3 v0 = _p2.transform.position - _p1.transform.position;
        Vector3 v1 = _p3.transform.position - _p1.transform.position;
        Vector3 v2 = _target.transform.position - _p1.transform.position;

        float dot00 = Vector3.Dot(v0, v0);
        float dot01 = Vector3.Dot(v0, v1);
        float dot02 = Vector3.Dot(v0, v2);
        float dot11 = Vector3.Dot(v1, v1);
        float dot12 = Vector3.Dot(v1, v2);

        float invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
        float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
        float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

        // 점이 삼각형 내부에 있으면 u와 v가 0 이상이고 u + v <= 1
        return (u >= 0) && (v >= 0) && (u + v <= 1);
    }


    public bool IsDist(Character _ch1, Character _ch2, float _dist)
    {
        float dist = Vector3.Distance(_ch1.transform.position, _ch2.transform.position);
        if (dist <= _dist)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
