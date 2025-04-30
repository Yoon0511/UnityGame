using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill_Storm : Skill
{
    public int Count;
    public GameObject Stome;
    public float DistX = 0.0f;
    public float DistXInterval = 0.0f;
    public float DistZ = 0.0f;
    public GameObject SpwanPoint;
    public override void UseSkill()
    {
        base.UseSkill();
        for(int i = 0;i<Count;++i)
        {
            GameObject obj =  Instantiate(Stome);
            float distx = DistX * (DistXInterval * i);

            obj.transform.position = SpwanPoint.transform.position;
            obj.transform.rotation = Owner.transform.rotation;
            obj.GetComponent<FireStorm>().Init(distx, DistZ,Atk);
        }
        base.SkillEnd();
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(transform.position, transform.position//+Vector3.forward * 15f);
    //}
}
