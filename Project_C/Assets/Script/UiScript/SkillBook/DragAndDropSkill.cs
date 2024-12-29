using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropSkill : MonoBehaviour
{
    [SerializeField]
    Image image;
    Skill skill;
    SkillBtn skillBtn;

    public void Init(Skill _skill)
    {
        skill = _skill;
        image.sprite = skill.SPRITE;
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

        skillBtn.InputSkill(skill);
    }
}
