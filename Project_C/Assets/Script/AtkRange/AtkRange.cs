using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AtkRange : PoolAble
{
    Material Material;
    public float DesiredTime = 2f;
    private float StretchProgress = 0f;
    public bool ScaleX;
    public bool ScaleZ;
    public ATKRANGE_TYPE ATKRANGE_TYPE;
    
    private float StartTime;
    public bool ACTIVE_CONTROL;

    bool IsRpcInstance;
    private void Start()
    {
        StartTime = Time.time;
        Material = GetComponent<MeshRenderer>().material;
    }

    private void OnEnable()
    {
        StartTime = Time.time;
    }

    public void Init(bool _isRpcInstance)
    {
        IsRpcInstance = _isRpcInstance;
    }
    private void FixedUpdate()
    {
        StartSizeUp();
    }

    public void StartSizeUp()
    {
        if(Material == null)
        {
            Material = GetComponent<MeshRenderer>().material;
            return;
        }
        float elapsedTime = Time.time - StartTime;
        //elapsedTime += Time.deltaTime;
        StretchProgress = Mathf.Clamp01(elapsedTime / DesiredTime);

        if (StretchProgress < 1.0f)
        {
            if(ATKRANGE_TYPE == ATKRANGE_TYPE.CIRCLE)
            {
                StretchProgress *= 0.5f;
            }
            Material.SetFloat("_Progress", StretchProgress);
            //Vector3 scale = DAMAGE_RANGE.transform.localScale;
            //scale.x = ScaleX ? StretchProgress : scale.x;
            //scale.z = ScaleZ ? StretchProgress : scale.z;
            //DAMAGE_RANGE.transform.localScale = scale;
        }

        if(IsRpcInstance)
        {
            if(IsStretchEnd())
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetDesiredTime(float _time)
    {
        DesiredTime = _time;
    }

    public float GetDesiredTime() { return DesiredTime; }
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
        //Vector3 scale = DAMAGE_RANGE.transform.localScale;
        //scale.x = ScaleX ? StretchProgress : scale.x;
        //scale.z = ScaleZ ? StretchProgress : scale.z;
        //DAMAGE_RANGE.transform.localScale = scale;
    }

    private void OnReset()
    {
        StretchProgress = 0f;
        //Vector3 scale = DAMAGE_RANGE.transform.localScale;
        //scale.x = ScaleX ? StretchProgress : scale.x;
        //scale.z = ScaleZ ? StretchProgress : scale.z;
        //DAMAGE_RANGE.transform.localScale = scale;
    }
}
