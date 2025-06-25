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
        ////다른 클라에게 스킬사용여부 전송
        //Shared.PhotonMgr.SendSkill(GetPhotonViewId(), CurrentSkill[_index].GetId(), _index);
        //
        ////스킬사용
        //UseSkill(_index,(int)STATE.ATTACK, CurrentSkill[_index].SkillMotion, CurrentSkill[_index].Instant);
        ////UseSkill(_index,CurrentSkill[_index].SkillMotion);
        //
        ////HP,MP정보 갱신
        //UpdateUnitFrame();

        int viewId = GetPhotonViewId();
        int skillId = CurrentSkill[_index].GetId();

        // 자기 자신 먼저 처리
        SetCurrentSkill(_index, skillId);
        UseOtherPlayerSkill(_index);

        // 다른 클라에게 스킬 전송
        Shared.PhotonMgr.SendSkill(viewId, skillId, _index);

        // UI 갱신
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