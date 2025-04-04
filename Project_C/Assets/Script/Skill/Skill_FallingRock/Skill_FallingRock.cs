using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FallingRock : Skill
{
    public GameObject AtkRangeCircle;
    public GameObject Rock;
    public int RockCount;
    public float ROCKMINSPEED = 3;
    public float ROCKMAXSPEED = 10;

    List<AtkRange> ListAtkRangeCircle = new List<AtkRange>();
    public override void UseSkill()
    {
        for(int i = 0;i < RockCount; i++)
        {
            float RandomRange = Random.Range(-15f, 15f);
            Vector3 randpos = new Vector3(RandomRange, 0, RandomRange);
            Vector3 pos = Owner.transform.position + randpos;
            pos.y = 0.5f;
            GameObject AtkCircle = Instantiate(AtkRangeCircle, pos, Quaternion.identity);
            AtkCircle.GetComponent<AtkRange>().SetDesiredTime(Random.Range(3.0f,10.0f));
            ListAtkRangeCircle.Add(AtkCircle.GetComponent<AtkRange>());
        }

        base.UseSkill();
        StartCoroutine(IFallingRock());
    }

    IEnumerator IFallingRock()
    {
        int i = 0;
        while(ListAtkRangeCircle.Count > 0)
        {
            i = i % ListAtkRangeCircle.Count;
            if (ListAtkRangeCircle[i].IsStretchEnd())
            {
                //µ¹ »ý¼º
                GameObject rockobj = Instantiate(Rock);
                float RockSpeed = Random.Range(ROCKMINSPEED, ROCKMAXSPEED);
                rockobj.GetComponent<Rock>().Init(ListAtkRangeCircle[i].transform.position, RockSpeed, Atk,Random.Range(4.0f,7.0f));
                
                Destroy(ListAtkRangeCircle[i].gameObject);
                ListAtkRangeCircle.RemoveAt(i);
            }
            else
            {
                ListAtkRangeCircle[i].StartSizeUp();
            }
            ++i;
            yield return null;
        }
        if(ListAtkRangeCircle.Count == 0)
        {
            base.SkillEnd();
            StopCoroutine(IFallingRock());
        }
    }
}
