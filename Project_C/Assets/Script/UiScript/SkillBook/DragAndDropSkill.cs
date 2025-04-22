using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropSkill : MonoBehaviour
{
    [SerializeField]
    Image Image;
    Skill Skill;
    SkillBtn SkillBtn;
    SkillBtn OrgSkillBtn;
    public void Init(Skill _Skill)
    {
        Skill = _Skill;
        OrgSkillBtn = null;
        Image.sprite = Shared.GameMgr.GetSpriteAtlas("Skill_Icons", Skill.SpriteName);
    }
    public void Init(Skill _Skill, SkillBtn _orgSkillbtn)
    {
        Skill = _Skill;
        OrgSkillBtn = _orgSkillbtn;
        Image.sprite = Shared.GameMgr.GetSpriteAtlas("Skill_Icons", Skill.SpriteName);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("TAG_SKILLBTN"))
        {
            if(SkillBtn == null)
            {
                SkillBtn = collision.GetComponent<SkillBtn>();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TAG_SKILLBTN"))
        {
            if (SkillBtn != null)
            {
                SkillBtn = null;
            }
        }
    }

    public void DropSkill()
    {
        if (SkillBtn == null) return;

        //SkillBtn.InputSkill(Skill);

        if (OrgSkillBtn == null) //스킬칸에 스킬이 없을때
        {
            SkillBtn.InputSkill(Skill);
        }
        else if(SkillBtn.IsSkillNull() == false &&
            OrgSkillBtn != null) //스킬칸에 다른 스킬이 있을때
        {
            SkillBtn.SkillSwap(OrgSkillBtn);
        }
    }
}
