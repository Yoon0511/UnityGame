using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn: MonoBehaviour
{
    public int SkillIndex;
    [SerializeField]
    Image Image;
    [SerializeField]
    Text CoolTimeText;
    Color Originalcolor = new Color(255, 255, 255,255);

    float SkillCooltime;
    bool IsUseSkill = true;

    Player player;
    private void Start()
    {
        player = Shared.GameMgr.PLAYER.GetComponent<Player>();
    }

    public void InputSkill(Skill _Skill)
    {
        player.SetCurrentSkill(SkillIndex, _Skill);

        Image.sprite = _Skill.Sprite;
        Image.color = Originalcolor;
        SkillCooltime = _Skill.CoolTime;
    }

    public void UseSkill()
    {
        if (IsUseSkill == false) 
            return;

        player.UseSkill(SkillIndex);

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
