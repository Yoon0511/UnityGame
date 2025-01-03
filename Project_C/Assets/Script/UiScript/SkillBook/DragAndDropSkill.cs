using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropSkill : MonoBehaviour
{
    [SerializeField]
    Image image;
    SkillBtn skillBtn;

    Skill skill;
    ISkill Iskill;
    public void Init(Skill _skill, ISkill _iskill)
    {
        skill = _skill;
        Iskill = _iskill;
        image.sprite = skill.Sprite;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("TAG_SKILLBTN"))
        {
            if(skillBtn == null)
            {
                skillBtn = collision.GetComponent<SkillBtn>();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TAG_SKILLBTN"))
        {
            if (skillBtn != null)
            {
                skillBtn = null;
            }
        }
    }

    public void DropSkill()
    {
        if (skillBtn == null) return;

        //skillBtn.InputSkill(skill);
        skillBtn.InputSkill(skill, Iskill);
    }
}
