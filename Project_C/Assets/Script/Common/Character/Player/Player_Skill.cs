using System.Collections;
using System.Collections.Generic;
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
}