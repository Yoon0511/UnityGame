using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AtkRange : MonoBehaviour
{
    public GameObject DAMAGE_RANGE;
    public float DesiredTime = 2f;
    private float StretchProgress = 0f;
    public bool ScaleX;
    public bool ScaleZ;

    private float StartTime;

    public bool ACTIVE_CONTROL;
    private void Start()
    {
        StartTime = Time.time;
    }

    private void OnEnable()
    {
        StartTime = Time.time;
    }

    public void StartSizeUp()
    {
        float elapsedTime = Time.time - StartTime;
        StretchProgress = Mathf.Clamp01(elapsedTime / DesiredTime);

        if (StretchProgress < 1.0f)
        {
            Vector3 scale = DAMAGE_RANGE.transform.localScale;
            scale.x = ScaleX ? StretchProgress : scale.x;
            scale.z = ScaleZ ? StretchProgress : scale.z;
            DAMAGE_RANGE.transform.localScale = scale;
        }
    }

    public void SetDesiredTime(float _time)
    {
        DesiredTime = _time;
    }
    public bool IsStretchEnd()
    {
        //if (StretchProgress < 1.0f)
        //    return false;
        //else
        //{
        //    OnReset();
        //    return true;
        //}

        return StretchProgress >= 1.0f;
    }

    private void OnDisable()
    {
        StretchProgress = 0f;
        Vector3 scale = DAMAGE_RANGE.transform.localScale;
        scale.x = ScaleX ? StretchProgress : scale.x;
        scale.z = ScaleZ ? StretchProgress : scale.z;
        DAMAGE_RANGE.transform.localScale = scale;
    }

    private void OnReset()
    {
        StretchProgress = 0f;
        Vector3 scale = DAMAGE_RANGE.transform.localScale;
        scale.x = ScaleX ? StretchProgress : scale.x;
        scale.z = ScaleZ ? StretchProgress : scale.z;
        DAMAGE_RANGE.transform.localScale = scale;
    }
}
