using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeUi : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField]
    Text cooltext;
    Color originalcolor = new Color(255, 255, 255);

    float skillCooltime;
    
    Skill skill;
   
    
    public void InputSkill(Skill _skill)
    {
        skill = _skill;
        image.sprite = Shared.GameMgr.GetSpriteAtlas("Common", _skill.SpriteName);
        skillCooltime = skill.CoolTime;
    }

    public void UseSkill()
    {
        //skill.UseSkill();
        cooltext.gameObject.SetActive(true);
        image.color = new Color(0, 0, 0);
        StartCoroutine(ICoolTime());
    }

    IEnumerator ICoolTime()
    {
        float time = 0.0f;

        while(time <= skillCooltime)
        {
            time += Time.deltaTime;
            
            yield return null;

            float T = time / skillCooltime;
            image.fillAmount = T;
            float textT = skillCooltime - time;
            cooltext.text = textT.ToString("F1");
            
            float tcolor = T * 255f;
            image.color = new Color(tcolor, tcolor, tcolor);

            if (time >= skillCooltime)
            {
                image.color = originalcolor;
                cooltext.gameObject.SetActive(false);
                StopCoroutine(ICoolTime());
            }
        }
    }
}
