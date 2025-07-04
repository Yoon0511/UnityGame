using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.U2D;

public class ParticleMgr : MonoBehaviour
{
    [SerializeField]
    GameObject Particles;
    public int MaxPoolSize = 10;
    public int LoadCount = 3;
    [NonReorderable]
    Dictionary<string, GameObject> DicParticleObjects = new Dictionary<string, GameObject>();
    string SelectParticleName;
    Dictionary<string, IObjectPool<GameObject>> DicParticlePool = new Dictionary<string, IObjectPool<GameObject>>();

    private void Awake()
    {
        Shared.ParticleMgr = this;
    }

    private void Start()
    {
        GameObject[] arrobj = Resources.LoadAll<GameObject>("Prefabs/Particle");
        foreach (GameObject obj in arrobj)
        {
            DicParticleObjects.Add(obj.name, obj);

            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(CreateParticle, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject,
           true, 5, MaxPoolSize);

            DicParticlePool.Add(obj.name, pool);

            for(int i = 0;i< LoadCount; ++i)
            {
                CreateParticle(obj.name, transform, 1.0f);
            }
        }
    }

    GameObject CreateParticle()
    {
        GameObject obj = Instantiate(DicParticleObjects[SelectParticleName]);
        obj.GetComponent<PoolAble>().Pool = DicParticlePool[SelectParticleName];
        obj.transform.SetParent(Particles.transform, false);
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

    public GameObject GetParticle(string _name)
    {
        SelectParticleName = _name;
        if (DicParticleObjects.ContainsKey(SelectParticleName) == false)
        {
            return null;
        }   
        return DicParticlePool[SelectParticleName].Get();
    }

    public GameObject CreateParticle(string _name,Transform _transform,float _duration)
    {
        GameObject obj = GetParticle(_name);
        obj.transform.position = _transform.position;
        obj.transform.rotation = _transform.rotation;
        obj.GetComponent<Particle>().Duration = _duration;
        return obj;
    }

    public void CreateParticle(string _name, Transform _transform, float _duration,Transform _parent)
    {
        GameObject obj = GetParticle(_name);
        obj.transform.position = _transform.position;
        obj.transform.rotation = _transform.rotation;
        obj.transform.SetParent(_parent);
        obj.GetComponent<Particle>().Duration = _duration;
    }
}
