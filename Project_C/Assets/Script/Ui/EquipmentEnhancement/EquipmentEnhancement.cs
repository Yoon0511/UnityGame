using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentEnhancement : MonoBehaviour
{
    public GameObject CONTENT;
    public EnhanceView ENHANCEVIEW;
    public GameObject ENHANCERESULT_EFFECT;
    public GameObject ENHANCERESULT_VIEW;
    public Image ENHANCERESULT_IMAGE;

    [SerializeField]
    EnhanceResultView EnhanceResultView;

    List<GameObject> ListEnhaceEquipItemSlot = new List<GameObject>();
    Player Player;

    private void OnEnable()
    {
        Init();
    }
    private void OnDisable()
    {
        EnhaceEquipSlotReset();
    }
    public void Init()
    {
        //장착중인 장비, 인벤에 있는 장비 가져와서 리스트 만들기
        if (Player == null)
        {
            Player = Shared.GameMgr.PLAYER;
        }
        Shared.SoundMgr.PlaySFX("UI_NOTIFICATION");

        foreach(EquipmentItem item in Player.GetDicEquitmentItem().Values)
        {
            CreateEnhaceEquipItemSlot(item);
        }

        foreach(ItemBase item in Player.GetInventory().GetItems())
        {
            if(item.ItemType == ITEM_TYPE.EQUIPMENT)
            {
                EquipmentItem equipment = item as EquipmentItem;
                if (equipment != null)
                {
                    CreateEnhaceEquipItemSlot(equipment);
                }
            }
        }

        float Count = ListEnhaceEquipItemSlot.Count;
        CONTENT.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Count * 105.0f);
    }

    void CreateEnhaceEquipItemSlot(EquipmentItem _equipitem)
    {
        GameObject EnhanceEquipItemSlot = Shared.PoolMgr.GetObject("EnhanceEquipItemSlot");
        EnhanceEquipItemSlot.transform.SetParent(CONTENT.transform, false);
        EnhanceEquipItemSlot.GetComponent<EnhanceEquipItemSlot>().Init(_equipitem, ENHANCEVIEW);
        ListEnhaceEquipItemSlot.Add(EnhanceEquipItemSlot);
    }

    void EnhaceEquipSlotReset()
    {
        for (int i = ListEnhaceEquipItemSlot.Count - 1; i >= 0; i--)
        {
            if (ListEnhaceEquipItemSlot[i] != null)
            {
                PoolAble poolAble = ListEnhaceEquipItemSlot[i].GetComponent<PoolAble>();
                if (poolAble != null)
                {
                    poolAble.ReleaseObject();
                    //ListEnhaceEquipItemSlot.RemoveAt(i);
                }
            }
        }
        ListEnhaceEquipItemSlot.Clear();
    }

    public void OnEnhanceEffect() // 1.강화하기 버튼 클릭 -> 결과창 이펙트 생성
    {
        int RequireGold = ENHANCEVIEW.GetEnhaceViewItem().GetEnhaceMaterial();
        
        if (Player.UseGold(RequireGold)) //골드 사용
        {
            //강화에 사용할 골드를 충분한 보유시
            ENHANCERESULT_EFFECT.SetActive(true);
            string SpriteName = ENHANCEVIEW.GetEnhaceViewItem().SpriteName;
            ENHANCERESULT_IMAGE.sprite = Shared.GameMgr.GetSpriteAtlas("Items", SpriteName);
        }
        else
        {
            //강화에 사용할 골드가 부족할시
            Shared.UiMgr.CreateSystemMsg("골드가 부족합니다.",SYSTEM_MSG_TYPE.UI);
        }
    }

    public void OnCheckResults() // 2.클릭시 결과 확인
    {
        ENHANCERESULT_VIEW.SetActive(true);

        Shared.GameMgr.PLAYER.ApplyEquipItem(ENHANCEVIEW.GetEnhaceViewItem(), true); //강화된 아이템 적용
        bool Result = ENHANCEVIEW.TryEnhance(); //강화시도

        EnhanceResultView.Init(ENHANCEVIEW.GetEnhaceViewItem(),Result); //결과전달
        Shared.GameMgr.PLAYER.ApplyEquipItem(ENHANCEVIEW.GetEnhaceViewItem()); //강화된 아이템 적용
    }

    public void OffEnhanceResult() //3.결과창 끔
    {
        ENHANCERESULT_EFFECT.SetActive(false);
        ENHANCERESULT_VIEW.SetActive(false);

        //장비슬롯초기화 및 갱신
        EnhaceEquipSlotReset();
        Init();

        ENHANCEVIEW.Refresh(); //강화뷰 갱신
    }
}
