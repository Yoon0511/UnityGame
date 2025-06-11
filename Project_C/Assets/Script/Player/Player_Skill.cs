using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [SerializeField]
    BuffUi BuffUi;

    public override void UseSkill(int _index)
    {
        //�ٸ� Ŭ�󿡰� ��ų��뿩�� ����
        Shared.PhotonMgr.SendSkill(GetPhotonViewId(), CurrentSkill[_index].GetId(), _index);

        //��ų���
        UseSkill(_index,(int)STATE.ATTACK, CurrentSkill[_index].SkillMotion, CurrentSkill[_index].Instant);
        //UseSkill(_index,CurrentSkill[_index].SkillMotion);

        //HP,MP���� ����
        UpdateUnitFrame();
    }

    public void UseOtherPlayerSkill(int _index)
    {
        UseSkill(_index, (int)STATE.ATTACK, CurrentSkill[_index].SkillMotion, CurrentSkill[_index].Instant);
    }
}