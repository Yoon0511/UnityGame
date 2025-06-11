using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [SerializeField]
    BuffUi BuffUi;

    public override void UseSkill(int _index)
    {
        //다른 클라에게 스킬사용여부 전송
        Shared.PhotonMgr.SendSkill(GetPhotonViewId(), CurrentSkill[_index].GetId(), _index);

        //스킬사용
        UseSkill(_index,(int)STATE.ATTACK, CurrentSkill[_index].SkillMotion, CurrentSkill[_index].Instant);
        //UseSkill(_index,CurrentSkill[_index].SkillMotion);

        //HP,MP정보 갱신
        UpdateUnitFrame();
    }

    public void UseOtherPlayerSkill(int _index)
    {
        UseSkill(_index, (int)STATE.ATTACK, CurrentSkill[_index].SkillMotion, CurrentSkill[_index].Instant);
    }
}