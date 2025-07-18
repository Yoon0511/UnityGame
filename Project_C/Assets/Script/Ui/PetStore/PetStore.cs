using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UIElements;

public class PetStore : MonoBehaviour
{
    public GameObject DrawPetItemPrefab;
    public GameObject DrawItemParent;
    public List<GameObject> DrawPetItems = new();

    int PetItemId = 0;

    private void OnEnable()
    {
        DrawItemsClear();
    }

    public void Buy(JSONNode _data)
    {
        float probability = float.Parse(_data[0]);
        ITEM_GRADE Grade = GetGrade(probability);

        GameObject DrawPetItem = Instantiate(DrawPetItemPrefab);
        DrawPetItem.transform.SetParent(DrawItemParent.transform, false);
        DrawPetItem.GetComponent<DrawPetItem>().Init(Grade, PetItemId);
        DrawPetItems.Add(DrawPetItem);
    }

    public void OnPetDrawOne()
    {
        DrawItemsClear();
        Action<JSONNode> PetBuy = Buy;
        Shared.ServerMgr.OnBtnConnect(PetBuy);
    }

    public void OnPetDrawTen()
    {
        DrawItemsClear();
        StartCoroutine(IPetDrawTen());
    }

    IEnumerator IPetDrawTen()
    {
        Action<JSONNode> PetBuy = Buy;
        for (int i = 0;i<10;++i)
        {
            Shared.ServerMgr.OnBtnConnect(PetBuy);
            yield return new WaitForSeconds(0.2f);
        }
    }

    //COMMON, - 30
    //UNCOMMON, - 25
    //RARE, - 20
    //EPIC, - 15
    //LEGENDARY, - 10

    ITEM_GRADE GetGrade(float _probability)
    {
        if (_probability <= 0.10f)
        {
            PetItemId = 1030;
            return ITEM_GRADE.LEGENDARY;
        }
        else if (_probability <= 0.25f)
        {
            PetItemId = 1029;
            return ITEM_GRADE.EPIC;
        }
        else if (_probability <= 0.45f)
        {
            PetItemId = 1028;
            return ITEM_GRADE.RARE;
        }
        else if (_probability <= 0.70f)
        {
            PetItemId = 1027;
            return ITEM_GRADE.UNCOMMON;
        }
        else
        {
            PetItemId = 1026;
            return ITEM_GRADE.COMMON;
        }

    }

    public void DrawItemsClear()
    {
        for(int i = DrawPetItems.Count - 1;i >= 0;i--)
        {
            Destroy(DrawPetItems[i]);
        }
        DrawPetItems.Clear();
    }
}
