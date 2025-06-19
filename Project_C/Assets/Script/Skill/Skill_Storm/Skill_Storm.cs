using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill_Storm : Skill
{
    public int Count;
    public GameObject Stome;
    public GameObject SpwanPoint;
    public override void UseSkill()
    {
        base.UseSkill();
        float AngleGap = 15.0f;
        Shared.SoundMgr.PlaySFX("FIRE_STOME");
        for(int i = 0;i<Count;++i)
        {
            float angleOffset = (i - (Count - 1) / 2f) * AngleGap;
            GameObject obj =  Instantiate(Stome, transform.position, Quaternion.identity);
            obj.GetComponent<FireStorm>().Init(Atk,angleOffset,Owner.transform.forward);
        }
        base.SkillEnd();
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(transform.position, transform.position//+Vector3.forward * 15f);
    //}
}
