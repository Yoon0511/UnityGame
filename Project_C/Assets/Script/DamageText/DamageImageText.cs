using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using ExitGames.Client.Photon;

public class DamageImageText : MonoBehaviour
{
    public GameObject PARENT;
    public GameObject DAMAGEFONT;
    public GameObject DAMAGENUMBER;
    public Sprite[] DamageImg;
    Canvas Canvas;

    IObjectPool<GameObject> DamageFontPool;
    IObjectPool<GameObject> DamageNumberPool;
    public int MaxPoolSize = 100;
    private void Start()
    {
        for (int i =0;i<10;++i)
        {
            DamageImg[i] = Shared.GameMgr.GetSpriteAtlas("Damage", i.ToString());
        }
        Canvas = Shared.GameMgr.CANVAS;

        //m_Pool = new ObjectPool<ParticleSystem>(CreatePooledItem, OnTakeFromPool,
        //OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);

        DamageFontPool = new ObjectPool<GameObject>(CreateDamageFont, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject,
            true,10,MaxPoolSize);
        DamageNumberPool = new ObjectPool<GameObject>(CreateDamageNumber, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject,
            true,10,MaxPoolSize);
    }
    GameObject CreateDamageNumber()
    {
        GameObject obj = Instantiate(DAMAGENUMBER);
        return obj;
    }
    GameObject CreateDamageFont()
    {
        GameObject obj = Instantiate(DAMAGEFONT);
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

    public void CreateDamageImage(int _damage, Vector3 _pos)
    {
        string damage = _damage.ToString();

        Vector3 pos = Camera.main.WorldToScreenPoint(_pos);

        GameObject PoolObj = DamageFontPool.Get();
        PoolObj.GetComponent<DamageImg>().Pool = DamageFontPool;
        PoolObj.GetComponent<DamageImg>().DamageNumberPool = DamageNumberPool;
        PoolObj.GetComponent<RectTransform>().anchoredPosition = pos;
        PoolObj.transform.SetParent(Canvas.transform);
        
        for (int i = 0; i < damage.Length; ++i)
        {
            GameObject PoolNumber = DamageNumberPool.Get();
            PoolObj.GetComponent<DamageImg>().AddDamageNumber(PoolNumber);
            PoolNumber.GetComponent<Image>().transform.SetParent(PoolObj.transform);
            int SpriteIndex = damage[i] - '0';
            PoolNumber.GetComponent<Image>().sprite = DamageImg[SpriteIndex];
        }
    }


    //public void CreateDamageImage(int _damage,Vector3 _pos)
    //{
    //    string damage = _damage.ToString();
    //
    //    Vector3 pos = Camera.main.WorldToScreenPoint(_pos);
    //
    //    GameObject obj = Instantiate(DAMAGEIMG);
    //    obj.GetComponent<RectTransform>().anchoredPosition = pos;
    //    obj.transform.SetParent(Canvas.transform);
    //
    //    for (int i = 0;i<damage.Length;++i)
    //    {
    //        Image img = Instantiate(DAMAGENUMBER).GetComponent<Image>();
    //        img.transform.SetParent(obj.transform);
    //        int SpriteIndex = damage[i] - '0';
    //        img.sprite = DamageImg[SpriteIndex];
    //    }
    //}
}
