using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Skill_Rush : Skill
{
    public float Speed;
    public float Distance;
    public override void UseSkill()
    {
        base.UseSkill();
        StartCoroutine(IRush());
    }

    IEnumerator IRush()
    {
        float dist = 0;
        Vector3 orgPos = Owner.transform.position;

        while (dist < Distance)
        {
            dist = Vector3.Distance(orgPos, Owner.transform.position);
            Owner.transform.Translate(Vector3.forward * Speed * Time.deltaTime);

            if (dist >= Distance)
            {
                base.SkillEnd();
                yield break;
            }
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Owner)
            return;
        
        if(Shared.GameMgr.IsCheckCharacterType(other,(int)CHARACTER_TYPE.PLAYER))
        {
            other.gameObject.GetComponent<Player>().Hit(Atk);
        }
    }
}
