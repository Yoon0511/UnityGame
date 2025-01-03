using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn: MonoBehaviour
{
    [SerializeField]
    Image Image;
    [SerializeField]
    Text CoolTimeText;
    Color Originalcolor = new Color(255, 255, 255,255);

    float SkillCooltime;
    bool IsUseSkill = true;

    Skill Skill;
    ISkill Iskill;

    public void InputSkill(Skill _Skill,ISkill _iskill)
    {
        Iskill = _iskill;
        Skill = _Skill;

        Image.sprite = Skill.Sprite;
        Image.color = Originalcolor;
        SkillCooltime = Skill.CoolTime;
    }

    public void UseSkill()
    {
        if (IsUseSkill == false || Skill == null) 
            return;

        Iskill.UseSkill();

        IsUseSkill = false;
        CoolTimeText.gameObject.SetActive(true);
        Image.color = new Color(0, 0, 0);
        StartCoroutine(ICoolTime());
    }
   
    IEnumerator ICoolTime()
    {
        float time = 0.0f;

        while(time <= SkillCooltime)
        {
            time += Time.deltaTime;
            
            yield return null;

            float T = time / SkillCooltime;
            Image.fillAmount = T;
            float textT = SkillCooltime - time;
            CoolTimeText.text = textT.ToString("F1");
            
            float tcolor = T * 255f;
            Image.color = new Color(tcolor, tcolor, tcolor);

            if (time >= SkillCooltime)
            {
                IsUseSkill = true;
                Image.color = Originalcolor;
                CoolTimeText.gameObject.SetActive(false);
                StopCoroutine(ICoolTime());
            }
        }
    }
}
