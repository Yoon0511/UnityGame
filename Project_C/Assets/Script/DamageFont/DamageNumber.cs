using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class DamageNumber : MonoBehaviour
{
    public float DeleteTime;
    public float Speed;
    public Vector3 Dir = Vector3.zero;

    Vector3 NomalDir;
    RectTransform RectTr;

    public IObjectPool<GameObject> Pool;
    public IObjectPool<GameObject> DamageNumberPool;
    List<GameObject> ListDamageNumber = new List<GameObject>();
    private void Awake()
    {
        RectTr = GetComponent<RectTransform>();
        NomalDir = Dir.normalized;
        //StartCoroutine(StartDamageImg());
    }

    private void OnEnable()
    {
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
        //pool¿¡ ¹ÝÈ¯
        Pool.Release(gameObject);
        
        foreach (GameObject ObjNumber in ListDamageNumber)
        {
            DamageNumberPool.Release(ObjNumber);
        }
        ListDamageNumber.Clear();
        //Destroy(gameObject);
    }

    public void AddDamageNumber(GameObject _number)
    {
        ListDamageNumber.Add(_number);
    }
}
