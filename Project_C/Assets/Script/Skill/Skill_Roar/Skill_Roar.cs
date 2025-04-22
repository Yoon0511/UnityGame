using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Roar : Skill
{
    public float Duration;
    public float Range;
    public float AtferDelay;
    public override void UseSkill()
    {
        base.UseSkill();
        StartCoroutine(IRoar());
    }

    IEnumerator IRoar()
    {
        Shared.MainCamera.Shake(0, 0, 1.0f, new Vector3(0.5f, 0.5f, 0.0f), 8f, 2f, 3);

        Transform PlayerTs = Shared.GameMgr.PLAYEROBJ.transform;
        float dist = Vector3.Distance(Owner.transform.position, PlayerTs.position);

        if (dist <= Range)
        {
            DeBuff Stun = new DeBuff_Stun(Duration, Shared.GameMgr.PLAYEROBJ, "UI_Skill_Icon_Blackhole");
            Shared.GameMgr.PLAYER.AddDeBuff(Stun);
        }
        yield return new WaitForSeconds(AtferDelay);
        base.SkillEnd();
    }
    void Roar()
    {
        Transform PlayerTs = Shared.GameMgr.PLAYEROBJ.transform;
        float dist = Vector3.Distance(Owner.transform.position, PlayerTs.position);

        if(dist <= Range)
        {
            DeBuff Stun = new DeBuff_Stun(Duration, Shared.GameMgr.PLAYEROBJ, "UI_Skill_Icon_Blackhole");
            Shared.GameMgr.PLAYER.AddDeBuff(Stun);
        }
        base.SkillEnd();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
