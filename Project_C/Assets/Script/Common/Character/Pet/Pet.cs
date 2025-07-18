using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : Character
{
    Coroutine BuffCourtine;
    public override void Fsm_Init()
    {

    }

    public override void Hit(DamageData _damagedata)
    {

    }

    public override void Init()
    {
        
    }

    public override void RayTargetEvent(Character _character)
    {
        
    }

    public override void UseSkill(int _index)
    {
        
    }

    //∆Í ¿Â¬¯Ω√ »£√‚
    public void Equip()
    {
        BuffCourtine = StartCoroutine(IPetBuff());
    }

    //∆Í «ÿ¡¶Ω√ »£√‚
    public void UnEquip()
    {
        StopCoroutine(BuffCourtine);
    }
    IEnumerator IPetBuff()
    {
        int RandomDef = Random.Range(1, 10);
        int RandomDuration = Random.Range(5, 10);

        StatBuff DefBuff = new StatBuff(STAT_TYPE.DEF, RandomDef, RandomDuration, Shared.GameMgr.PLAYEROBJ, "UI_Skill_Icon_DefBuff");
        Shared.GameMgr.PLAYER.AddBuff(DefBuff);

        yield return new WaitForSeconds(RandomDuration);

        StartCoroutine(IPetBuff());
    }
}
