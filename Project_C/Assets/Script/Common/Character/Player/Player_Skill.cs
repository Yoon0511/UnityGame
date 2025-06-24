using System.Collections;
using System.Collections.Generic;
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
}