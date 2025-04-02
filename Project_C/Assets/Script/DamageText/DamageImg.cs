using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageImg : MonoBehaviour
{
    public float DeleteTime;
    public float Speed;
    public Vector3 Dir = Vector3.zero;

    Vector3 NomalDir;
    RectTransform RectTr;
    private void Start()
    {
        RectTr = GetComponent<RectTransform>();
        NomalDir = Dir.normalized;
        StartCoroutine(StartDamageImg());
    }

    IEnumerator StartDamageImg()
    {
        float dt = 0.0f;
        while (dt <= DeleteTime)
        {
            dt += Time.deltaTime;
            RectTr.anchoredPosition += (Vector2)(NomalDir * Speed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
