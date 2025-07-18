using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class STATENHACE
{
    public float value;
    public StatEnhace StatEnhace;
}

public class InfoPlayer : MonoBehaviour
{
    public Text NAME;
    public Text ATK;
    public Text DEF;
    public Text SPEED;
    public Text StatPoint;
    public Dictionary<STAT_TYPE, STATENHACE> DicStatEnhance = new();
    public StatEnhace[] StatEnhaces;
    public StatCompletePopup StatCompletePopup;
    public GameObject StatEnahceUi;

    Player player;
    bool StatEnchaceComplete;

    private void Start()
    {
        StatEnchaceComplete = false;
        for(int i = 0;i<StatEnhaces.Length;++i)
        {
            STATENHACE statenhance = new();
            statenhance.value = 0f;
            statenhance.StatEnhace = StatEnhaces[i];
            DicStatEnhance.Add(StatEnhaces[i].STAT_TYPE, statenhance);
        }

        StartCoroutine(IAutoRefresh());
    }

    IEnumerator IAutoRefresh()
    {
        Refresh();
        yield return new WaitForSeconds(1f);
        StartCoroutine(IAutoRefresh());
    }

    public void Refresh()
    {
        player = Shared.GameMgr.PLAYER;

        if(player == null )
        {
            return;
        }

        NAME.text = "<color=#C0C0C0>" + player.GetCharacterName() + "</color>";

        string EnhanceColor = null;
        TextColor(DicStatEnhance[STAT_TYPE.ATK].value, out EnhanceColor);
        ATK.text = $"<color=#{player.GetStatColor(STAT_TYPE.ATK)}><b>ATK</b></color> : <color=#{EnhanceColor}>{player.GetInStatData(STAT_TYPE.ATK) + DicStatEnhance[STAT_TYPE.ATK].value}</color>";
        int bonusStat = (int)(player.GetStatData().GetStatBonus(STAT_TYPE.ATK));
        if(bonusStat > 0)
        {
            ATK.text += $"<color=#00E676><b> +{bonusStat}</b></color>";
        }

        TextColor(DicStatEnhance[STAT_TYPE.DEF].value, out EnhanceColor);
        DEF.text = $"<color=#{player.GetStatColor(STAT_TYPE.DEF)}><b>DEF</b></color> : <color=#{EnhanceColor}>{player.GetInStatData(STAT_TYPE.DEF) + DicStatEnhance[STAT_TYPE.DEF].value}</color>";
        bonusStat = (int)(player.GetStatData().GetStatBonus(STAT_TYPE.DEF));
        if (bonusStat > 0)
        {
            DEF.text += $"<color=#00E676><b> +{bonusStat}</b></color>";
        }

        TextColor(DicStatEnhance[STAT_TYPE.SPEED].value, out EnhanceColor);
        SPEED.text = $"<color=#{player.GetStatColor(STAT_TYPE.SPEED)}><b>SPEED</b></color> : <color=#{EnhanceColor}>{player.GetInStatData(STAT_TYPE.SPEED) + DicStatEnhance[STAT_TYPE.SPEED].value}</color>";
        bonusStat = (int)(player.GetStatData().GetStatBonus(STAT_TYPE.SPEED));
        if (bonusStat > 0)
        {
            SPEED.text += $"<color=#00E676><b> +{bonusStat}</b></color>";
        }

        StatPoint.text = $"<color=#FFD700><b>{player.GetStatPoint().ToString()}</b></color>";
    
        if(player.GetStatPoint() > 0)
        {
            StatEnchaceComplete = false;
            StatEnahceUi.SetActive(true);
        }
        else if(player.GetStatPoint() <= 0 && StatEnchaceComplete)
        {
            StatEnahceUi.SetActive(false);
        }
    }

    public void OnStatCompletePopup()
    {
        StatCompletePopup.gameObject.SetActive(true);
        StatCompletePopup.Init(this);
    }

    public void OnStatEnhaceComplete()
    {
        StatEnchaceComplete = true;
        StatCompletePopup.gameObject.SetActive(false);

        for(int i = 0;i<(int)STAT_TYPE.ENUM_END;++i)
        {
            STAT_TYPE type = (STAT_TYPE)i;
            if (DicStatEnhance.TryGetValue(type, out STATENHACE stat))
            {
                player.EnhanceStat(type, stat.value);
                DicStatEnhance[type].value = 0f;
                DicStatEnhance[type].StatEnhace.Refresh();
            }
        }
        player.StatComplete();

        Refresh();
    }

    public void TextColor(float _stat, out string _color)
    {
        int stat = (int)_stat;
        if(_stat <= 0)
        {
            _color = "FFFFFF";
        }
        else
        {
            _color = "FF0000";
        }
    }
}
