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
    Skill Skill;
    public void Init(Skill _Skill)
    {
        Skill = _Skill;
        image.sprite = Shared.GameMgr.GetSpriteAtlas("Common", _Skill.SpriteName);
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

        skillBtn.InputSkill(Skill);
    }
}
