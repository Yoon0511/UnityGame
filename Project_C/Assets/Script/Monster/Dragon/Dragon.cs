using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Dragon : Monster
{
   
    private void FixedUpdate()
    {
        if (ListPlayer == null)
        {
            //player = Shared.GameMgr.PLAYEROBJ;
            ListPlayer = Shared.GameMgr.GetListPLayer();
            return;
        }
        if (PV == null)
        {
            PV = gameObject.GetComponent<PhotonView>();
            return;
        }

        if (PV.IsMine == false)
        {
            return;
        }
        
        StateUpdate();
        AlignToTerrainHeight();
    }
    
    public override void Hit(DamageData _damagedata)
    {
        Shake(0.2f, 0.05f);
        Statdata.TakeDamage(_damagedata);

        CheckHP();
    }

    public override void Init()
    {
        CharacterName = "Dragon";
        CharacterType = (int)CHARACTER_TYPE.MONSTER;
        Id = (int)MONSTER_ID.DRAGON;
        transform.localEulerAngles = new Vector3(0, 180f, 0);
        SetIsDead(false);
        SkillInit();
        Fsm_Init();
    }

    public override void RayTargetEvent(Character _character)
    {
        Shared.GameMgr.Hphud.SetTarget(this);
    }

    protected override bool CheckHP()
    {
        if(GetIsDead())
        {
            return false;
        }

        if (Statdata.GetData(STAT_TYPE.HP) <= 0)
        {
            SendQuestMsg();
            ChangeState((int)DRAGON_STATE.DIE);
            return true;
        }
        return false;
    }
}
