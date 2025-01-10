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

    Player player;
    private void Start()
    {
        player = Shared.GameMgr.PLAYER;
    }

    public void InputSkill(Skill _Skill)
    {
        player.SetCurrentSkill(SkillIndex, _Skill);

        Image.sprite = Shared.GameMgr.GetSpriteAtlas("Common", _Skill.SpriteName);
        Image.color = Originalcolor;
    }

    public void UseSkill()
    {
        player.UseSkill(SkillIndex);

        if (player.IsCurrentSkillNull(SkillIndex) == false)
        {
            CoolTimeText.gameObject.SetActive(true);
            Image.color = new Color(0, 0, 0);
            StartCoroutine(ICoolTime());
        }
    }
   
    IEnumerator ICoolTime()
    {
        float Cooltime = player.GetSkillCoolTime(SkillIndex);
        float SkillCooltime = player.GetCurrentSkillCoolTime(SkillIndex);
        while (Cooltime > 0f)
        {
            Cooltime = player.GetSkillCoolTime(SkillIndex);

            float T = (SkillCooltime - Cooltime) / SkillCooltime;
            Image.fillAmount = T;
            float textT = Cooltime;
            CoolTimeText.text = textT.ToString("F1");
            
            float tcolor = T * 255f;
            Image.color = new Color(tcolor, tcolor, tcolor);

            if (Cooltime <= 0f)
            {
                Image.color = Originalcolor;
                CoolTimeText.gameObject.SetActive(false);
                StopCoroutine(ICoolTime());
            }
            yield return null;
        }
    }
}
