using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    bool CameraShake = false;

    Transform ShakeTr;

    public class cShakeInfo
    {
        public float StartDelay;

        public bool UseTotalTime;
        public float TotalTime;
        public Vector3 Dest;
        public Vector3 Shake;
        public Vector3 Dir;

        public float RemainDist;
        public float RemainCountDis;

        public bool UseCount;
        public int Count;

        public float Veclocity;

        public bool UseDamping;
        public float Damping;
        public float DampingTime;
    }

    cShakeInfo ShakeInfo = new cShakeInfo();

    Vector3 OrgPos;

    float FovX = 0.2f;
    float FovY = 0.2f;

    private void Awake()
    {
        Shared.MainCamera = this;

        OrgPos = transform.position;

        InitShake();
    }

    protected void InitShake()
    {
        ShakeTr = transform.parent;
        CameraShake = false;
    }

    protected void ResetShakeTr()
    {
        ShakeTr.localPosition = Vector3.zero;
        CameraShake = false;

        CameraLimit();
    }

    void CameraLimit(bool OrgPosY = false)
    {
        Vector3 camera = OrgPos;

        if (camera.x - FovX < 1)
            camera.x = 1 + FovX;
        else if (camera.x + FovX > -1)
            camera.x = -1 - FovX;

        if (OrgPosY)
            camera.y = OrgPos.y;
    }

    public void Shake( int CameraID )
    {
        //if (false == IsFallowMe)
        //    return;

        ShakeInfo.StartDelay = 0f;
        ShakeInfo.TotalTime = 3f;
        ShakeInfo.UseTotalTime = true;

        ShakeInfo.Shake = new Vector3(0.2f, 0.2f, 0f);

        ShakeInfo.Dest = ShakeInfo.Shake;
        ShakeInfo.Dir = ShakeInfo.Shake;
        ShakeInfo.Dir.Normalize();

        ShakeInfo.RemainDist = ShakeInfo.Shake.magnitude;
        ShakeInfo.RemainCountDis = float.MaxValue;

        ShakeInfo.Veclocity = 8;

        ShakeInfo.Damping = 0.5f;
        ShakeInfo.UseDamping = true;

        ShakeInfo.DampingTime = ShakeInfo.RemainDist / ShakeInfo.Veclocity;

        ShakeInfo.Count =4;
        ShakeInfo.UseCount = true;

        StopCoroutine("ShakeCoroutine");
        ResetShakeTr();
        StartCoroutine("ShakeCoroutine");
    }

    IEnumerator ShakeCoroutine()
    {
        CameraShake = true;

        float dt, dist;

        if (ShakeInfo.StartDelay > 0)
            yield return new WaitForSeconds(ShakeInfo.StartDelay);

        while(true)
        {
            dt = Time.fixedDeltaTime;
            dist = dt * ShakeInfo.Veclocity;

            if((ShakeInfo.RemainDist -= dist) > 0 )
            {
                ShakeTr.localPosition += ShakeInfo.Dir * dist;

                float rc = transform.position.x - FovX - 1;

                if (rc < 0)
                    ShakeTr.localPosition += new Vector3(-rc, 0, 0);

                rc = -1 - (transform.position.x + FovX);

                if (rc < 0)
                    ShakeTr.localPosition += new Vector3(rc, 0, 0);

                CameraLimit(true);

                if(ShakeInfo.UseCount)
                {
                    if((ShakeInfo.RemainCountDis -= dist) < 0)
                    {
                        ShakeInfo.RemainCountDis = float.MaxValue;

                        if (--ShakeInfo.Count < 0)
                            break;
                    }
                }
            }
            else
            {
                if(ShakeInfo.UseDamping)
                {
                    float distdamping = Mathf.Max(ShakeInfo.Damping * ShakeInfo.DampingTime,
                        ShakeInfo.Damping * dt);

                    if (ShakeInfo.Shake.magnitude > distdamping)
                        ShakeInfo.Shake -= ShakeInfo.Dir * distdamping;
                    else
                    {
                        ShakeInfo.UseCount = true;
                        ShakeInfo.Count = 1;
                    }
                }

                ShakeTr.localPosition = ShakeInfo.Dest - ShakeInfo.Dir * (-ShakeInfo.RemainDist);

                float rc = transform.position.x - FovX - 1;

                if (rc < 0)
                    ShakeTr.localPosition += new Vector3(-rc, 0, 0);

                rc = -1 - (transform.position.x + FovX);

                if (rc < 0)
                    ShakeTr.localPosition += new Vector3(rc, 0, 0);

                CameraLimit(true);

                ShakeInfo.Shake = -ShakeInfo.Shake;
                ShakeInfo.Dest = ShakeInfo.Shake;
                ShakeInfo.Dir = -ShakeInfo.Dir;

                float len = ShakeInfo.Shake.magnitude;

                ShakeInfo.RemainCountDis = len + ShakeInfo.RemainDist;
                ShakeInfo.RemainDist += len * 2f;

                ShakeInfo.DampingTime = ShakeInfo.RemainDist / ShakeInfo.Veclocity;

                if (ShakeInfo.RemainDist < dist)
                    break;
            }

            if (ShakeInfo.UseTotalTime && (ShakeInfo.TotalTime -= dt) < 0)
                break;

            yield return new WaitForFixedUpdate();
        }

        ResetShakeTr();

        yield break;
    }
}
