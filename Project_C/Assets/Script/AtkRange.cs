using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkRange : MonoBehaviour
{
    public GameObject DAMAGE_RANGE;
    public float DesiredTime = 2f;
    private float StretchSpeed = 1f;
    private float StretchProgress = 0f;
    public bool ScaleX;
    public bool ScaleZ;

    private void Start()
    {
        StretchSpeed = 1f / DesiredTime;
    }

    public void StartSizeUp()
    {
        StretchProgress += Time.deltaTime * StretchSpeed;
        StretchProgress = Mathf.Clamp01(StretchProgress);

        if (StretchProgress < 1.0f)
        {
            Vector3 scale = DAMAGE_RANGE.transform.localScale;
            scale.x = ScaleX ? StretchProgress : scale.x;
            scale.z = ScaleZ ? StretchProgress : scale.z;
            DAMAGE_RANGE.transform.localScale = scale;
        }
    }

    public bool IsStretchEnd()
    {
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
}
