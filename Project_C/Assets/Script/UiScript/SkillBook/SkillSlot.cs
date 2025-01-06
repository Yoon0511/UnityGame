using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    Skill skill;
    [SerializeField]
    Text skillText;
    [SerializeField]
    Image skillImg;

    Canvas canvas;
    public GameObject DragAndDropSkill;
    GameObject dragobj;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    public void InputSkill(Skill _skill)
    {
        skill = _skill;

        string text_name = "<color=white>" + skill.SkillName.ToString() + "</color>";
        string text_cooltime = "<color=orange>" + " (ÄðÅ¸ÀÓ:" + skill.CoolTime.ToString() + ")" + "</color>";
        string text_atk = "<color=red>" + "ATK : " + skill.Atk.ToString() + "</color>"; 
        string text_usemp = "<color=blue>" + "MP : " + skill.UseMp.ToString() + "</color>";
        string text_explanation = "<color=grey>" + "MP : " + skill.Explanation.ToString() + "</color>";
        skillText.text = text_name + text_cooltime + "\n" + text_atk + "\n" + text_usemp + "\n" + text_explanation;

        skillImg.sprite = skill.Sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragobj = Instantiate(DragAndDropSkill);
        dragobj.GetComponent<DragAndDropSkill>().Init(skill);
        dragobj.transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragobj.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragobj.GetComponent<DragAndDropSkill>().DropSkill();
        Destroy(dragobj);
    }
}
