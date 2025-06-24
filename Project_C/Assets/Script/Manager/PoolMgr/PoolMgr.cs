using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.ParticleSystem;

public class PoolMgr : MonoBehaviour
{
    [SerializeField]
    GameObject PoolObjs;
    public int MaxPoolSize = 30;
    public int LoadCount = 3;
    [NonReorderable]
    Dictionary<string, GameObject> DicPoolObjects = new Dictionary<string, GameObject>();
    Dictionary<string, IObjectPool<GameObject>> DicObjectsPool = new Dictionary<string, IObjectPool<GameObject>>();
    string SelectObjectName;

    private void Awake()
    {
        Shared.PoolMgr = this;
    }

    private void Start()
    {
        GameObject[] arrobj = Resources.LoadAll<GameObject>("Prefabs/PoolObj");
        foreach (GameObject obj in arrobj)
        {
            DicPoolObjects.Add(obj.name, obj);
            
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(CreateObject, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject,
           true, LoadCount, MaxPoolSize);

            DicObjectsPool.Add(obj.name, pool);

            // LoadCount 만큼 미리 생성
            for (int i = 0; i < LoadCount; ++i)
            {
                SelectObjectName = obj.name;
                GameObject temp = CreateObject();
                DicObjectsPool[obj.name].Release(temp);
            }
        }
    }

    GameObject CreateObject()
    {
        GameObject obj = Instantiate(DicPoolObjects[SelectObjectName]);
        obj.GetComponent<PoolAble>().Pool = DicObjectsPool[SelectObjectName];
        obj.transform.SetParent(PoolObjs.transform, false);
        return obj;
    }
    void OnReturnedToPool(GameObject _obj)
    {
        _obj.gameObject.SetActive(false);
    }

    void OnTakeFromPool(GameObject _obj)
    {
        _obj.gameObject.SetActive(true);
    }

    void OnDestroyPoolObject(GameObject _obj)
    {
        Destroy(_obj.gameObject);
    }

    public GameObject GetObject(string _name)
    {
        SelectObjectName = _name;

        if (DicPoolObjects.ContainsKey(SelectObjectName) == false)
        {
            return null;
        }
        return DicObjectsPool[SelectObjectName].Get();
    }
}
