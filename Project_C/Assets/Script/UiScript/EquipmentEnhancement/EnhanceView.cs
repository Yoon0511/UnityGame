using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceView : MonoBehaviour
{
    public Image EQUIP_IMG;
    public Text EQUIP_NAME;
    public Text EQUIPSTAT_PREVIEW;
    public Text ENHANCE_REQUIREGOLD;
    public Text PLAYERGOLD;
    public Text ENHANCEBTN_TEXT;

    EquipmentItem Item;

    public void Init(EquipmentItem _item)
    {
        Item = _item;

        EQUIP_IMG.sprite = Shared.GameMgr.GetSpriteAtlas("Items", Item.SpriteName);
        EQUIP_NAME.text = "+" + Item.GetEnhanceValue().ToString() + " " + Item.GetItemName();
        EQUIPSTAT_PREVIEW.text = Item.GetEnhanceInfo();
        UpdateMaterialGoldText();
    }

    public bool TryEnhance()
    {
        bool Enhance = Item.Enhance();
        return Enhance;
    }

    public void Refresh()
    {
        Init(Item);
    }

    void UpdateMaterialGoldText()
    {
        int RequireGold = Item.GetEnhaceMaterial();
        int PlayerGold = Shared.GameMgr.PLAYER.GetGold();
        string HaveText = "<color=#00BFFF><b>" + "�������" + "</b></color>" + "\n"; //�ϴû�
        string RequireText = "<color=#00BFFF><b>" + "�ʿ���" + "</b></color>" + "\n"; //�ϴû�
        string RequireGoldText = "<color=#FFD700>" + RequireGold.ToString() + "</color>"; //Ȳ�ݻ�

        ENHANCE_REQUIREGOLD.text = RequireText + RequireGoldText;

        if (PlayerGold < RequireGold)
        {
            //������� ���� - ������
            string StrPlayerGold = "<color=#FF0044>" + PlayerGold.ToString() + "</color>";
            PLAYERGOLD.text = HaveText + StrPlayerGold;

            // ��ư text ���� - ���������� ���� ����
            ENHANCEBTN_TEXT.text = "<color=#FF7070>" + "������" + "</color>";
        }
        else
        {
            //Ȳ�ݻ�
            string StrPlayerGold = "<color=#FFD700>" + PlayerGold.ToString() + "</color>";
            PLAYERGOLD.text = HaveText + StrPlayerGold;

            ENHANCEBTN_TEXT.text = "��ȭ�ϱ�";
        }
    }


    public EquipmentItem GetEnhaceViewItem() { return Item; }
}
