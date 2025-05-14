using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceResultView : MonoBehaviour
{
    public Text TITLE;
    public Image EQUIPIMG;
    public Text STATVIEW;

    EquipmentItem Item;

    public void Init(EquipmentItem _item, bool _result)
    {
        Item = _item;
        EQUIPIMG.sprite = Shared.GameMgr.GetSpriteAtlas("Items", Item.SpriteName);
        SetTitle(_result);
        SetStatView(_result);
    }

    void SetTitle(bool _result)
    {
        if (_result) //��ȭ ����
        {
            TITLE.text = $"<color=#FFD700><b>{Item.GetEnhanceValue()}�� ��ȭ ����!!</b></color>";
        }
        else //��ȭ ����
        {
            TITLE.text = $"<color=#B22222><b>{Item.GetEnhanceValue()}�� ��ȭ ����</b></color>";
        }
    }

    void SetStatView(bool _result)
    {
        float ItemStat = Item.GetItemStat();
        string StrItemStat = "<color=#CCCCCC>" + ItemStat.ToString("F0") + "</color>";
        string Plus = "<color=#FFA500><b>  =>  </b></color>";
        float EnhanceStat = ItemStat + Item.GetEnhanceRisingAmount();
        string StrStatType = "<color=#FFFFFF>" + Item.GetStrSTAT_Type() + " " + "</color>";

        if (_result) //���� ���
        {
            //���޶��� �׸�
            string StrEnhanceStat = "<color=#00D26A><b>" + EnhanceStat.ToString("F0") + "</b></color>";
            STATVIEW.text = StrStatType + StrItemStat + Plus + StrEnhanceStat;
        }
        else // ���� ����
        {
            //����� ����
            string StrEnhanceStat = "<color=#BBBBBB><b>" + EnhanceStat.ToString("F0") + "</b></color>";
            STATVIEW.text = StrStatType + StrItemStat + Plus + StrEnhanceStat;
        }
    }
}
