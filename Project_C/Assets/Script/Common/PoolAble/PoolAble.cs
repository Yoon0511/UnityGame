using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolAble : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }
    public virtual void ReleaseObject()
    {
        Pool.Release(gameObject);
    }
}
