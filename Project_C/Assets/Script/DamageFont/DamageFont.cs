using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using ExitGames.Client.Photon;
using UnityEngine.Rendering.PostProcessing;
using System;

public class DamageFont : MonoBehaviour
{
    public GameObject PARENT;
    public GameObject DAMAGEFONT;
    public GameObject DAMAGENUMBER;
    public Sprite[] DamageImg;
    Canvas Canvas;

    IObjectPool<GameObject> DamageFontPool;
    IObjectPool<GameObject> DamageNumberPool;
    public int MaxPoolSize = 100;

    Dictionary<DAMAGEFONT_TYPE, Sprite[]> DicDamgeFont = new Dictionary<DAMAGEFONT_TYPE, Sprite[]>();
    private void Start()
    {
        Sprite[] arrSprite = Resources.LoadAll<Sprite>("DamageFont/");

        int index = 0;
        foreach (DAMAGEFONT_TYPE type in Enum.GetValues(typeof(DAMAGEFONT_TYPE)))
        {
            if (type == DAMAGEFONT_TYPE.NONE || type == DAMAGEFONT_TYPE.ENUM_END) continue;

            Sprite[] numberSprites = new Sprite[10];
            for (int j = 0; j < 10; ++j)
            {
                numberSprites[j] = Shared.GameMgr.GetSpriteAtlas("DamageFont", arrSprite[index].name);
                index++;
            }

            DicDamgeFont[type] = numberSprites;
        }


        //for (int i =0;i<10;++i)
        //{
        //    DamageImg[i] = Shared.GameMgr.GetSpriteAtlas("Damage", i.ToString());
        //}

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

    public void CreateDamageFont(DamageData _damagedata, Vector3 _pos)
    {
        string strdamage = _damagedata.Damage.ToString();

        Vector3 pos = Camera.main.WorldToScreenPoint(_pos);

        GameObject PoolObj = DamageFontPool.Get();
        PoolObj.transform.SetParent(Canvas.transform,false);
        PoolObj.GetComponent<DamageNumber>().Pool = DamageFontPool;
        PoolObj.GetComponent<DamageNumber>().DamageNumberPool = DamageNumberPool;
        PoolObj.GetComponent<RectTransform>().anchoredPosition = pos;
        
        for (int i = 0; i < strdamage.Length; ++i)
        {
            GameObject PoolNumber = DamageNumberPool.Get();
            PoolNumber.transform.SetParent(PoolObj.transform,false);
            PoolNumber.transform.SetAsLastSibling();
            int SpriteIndex = strdamage[i] - '0';
            //PoolNumber.GetComponent<Image>().sprite = DamageImg[SpriteIndex];
            PoolNumber.GetComponent<Image>().sprite = GetDamageFontSprite(_damagedata.DamageFont_Type, SpriteIndex);
            PoolObj.GetComponent<DamageNumber>().AddDamageNumber(PoolNumber);
        }
    }

    public Sprite GetDamageFontSprite(DAMAGEFONT_TYPE type, int number)
    {
        if (!DicDamgeFont.ContainsKey(type)) return null;
        if (number < 0 || number >= DicDamgeFont[type].Length) return null;

        return DicDamgeFont[type][number];
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
