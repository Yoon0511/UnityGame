using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.Cockpit;
using UnityEngine;

public partial class Player : Character
{
    [SerializeField]
    BuffUi BuffUi;

    public override void UseSkill(int _index)
    {
        ////�ٸ� Ŭ�󿡰� ��ų��뿩�� ����
        //Shared.PhotonMgr.SendSkill(GetPhotonViewId(), CurrentSkill[_index].GetId(), _index);
        //
        ////��ų���
        //UseSkill(_index,(int)STATE.ATTACK, CurrentSkill[_index].SkillMotion, CurrentSkill[_index].Instant);
        ////UseSkill(_index,CurrentSkill[_index].SkillMotion);
        //
        ////HP,MP���� ����
        //UpdateUnitFrame();

        int viewId = GetPhotonViewId();
        int skillId = CurrentSkill[_index].GetId();

        // �ڱ� �ڽ� ���� ó��
        SetCurrentSkill(_index, skillId);
        UseOtherPlayerSkill(_index);

        // �ٸ� Ŭ�󿡰� ��ų ����
        Shared.PhotonMgr.SendSkill(viewId, skillId, _index);

        // UI ����
        UpdateUnitFrame();
    }

    public void UseOtherPlayerSkill(int _index)
    {
        UseSkill(_index, (int)STATE.ATTACK, CurrentSkill[_index].SkillMotion, CurrentSkill[_index].Instant);
    }

    public CurrSkillJson CurrSkillToJson()
    {
        CurrSkillJson json = new CurrSkillJson();

        for(int i = 0; i<CurrentSkill.Count;++i)
        {
            if(CurrentSkill[i] == null)
            {
                json.ListCurrSkillId.Add((int)PLAYER_SKILL_ID.NONE);
            }
            else
            {
                json.ListCurrSkillId.Add(CurrentSkill[i].GetId());
            }
        }

        return json;
    }

    public void ApplyCurrSkillData(CurrSkillJson _json)
    {
        for(int i = 0; i< _json.ListCurrSkillId.Count; ++i)
        {
            if (_json.ListCurrSkillId[i] != (int)PLAYER_SKILL_ID.NONE)
            {
                int skillid = _json.ListCurrSkillId[i];
                Shared.UiMgr.SkillBtn[i].InputSkill(GetSkill(skillid));
            }
        }
    }
}