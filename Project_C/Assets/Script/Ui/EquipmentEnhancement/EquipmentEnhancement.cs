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
        //�������� ���, �κ��� �ִ� ��� �����ͼ� ����Ʈ �����
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

    public void OnEnhanceEffect() // 1.��ȭ�ϱ� ��ư Ŭ�� -> ���â ����Ʈ ����
    {
        int RequireGold = ENHANCEVIEW.GetEnhaceViewItem().GetEnhaceMaterial();
        
        if (Player.UseGold(RequireGold)) //��� ���
        {
            //��ȭ�� ����� ��带 ����� ������
            ENHANCERESULT_EFFECT.SetActive(true);
            string SpriteName = ENHANCEVIEW.GetEnhaceViewItem().SpriteName;
            ENHANCERESULT_IMAGE.sprite = Shared.GameMgr.GetSpriteAtlas("Items", SpriteName);
        }
        else
        {
            //��ȭ�� ����� ��尡 �����ҽ�
            Shared.UiMgr.CreateSystemMsg("��尡 �����մϴ�.",SYSTEM_MSG_TYPE.UI);
        }
    }

    public void OnCheckResults() // 2.Ŭ���� ��� Ȯ��
    {
        ENHANCERESULT_VIEW.SetActive(true);

        Shared.GameMgr.PLAYER.ApplyEquipItem(ENHANCEVIEW.GetEnhaceViewItem(), true); //��ȭ�� ������ ����
        bool Result = ENHANCEVIEW.TryEnhance(); //��ȭ�õ�

        EnhanceResultView.Init(ENHANCEVIEW.GetEnhaceViewItem(),Result); //�������
        Shared.GameMgr.PLAYER.ApplyEquipItem(ENHANCEVIEW.GetEnhaceViewItem()); //��ȭ�� ������ ����
    }

    public void OffEnhanceResult() //3.���â ��
    {
        ENHANCERESULT_EFFECT.SetActive(false);
        ENHANCERESULT_VIEW.SetActive(false);

        //��񽽷��ʱ�ȭ �� ����
        EnhaceEquipSlotReset();
        Init();

        ENHANCEVIEW.Refresh(); //��ȭ�� ����
    }
}
