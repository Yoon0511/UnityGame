using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    public GameObject PREFAB_SKILLSLOT;

    [SerializeField]
    GameObject ParentContent;

    public void AddSkill(Skill _skill,ISkill _iskill)
    {
        GameObject skillSlot = Instantiate(PREFAB_SKILLSLOT, ParentContent.transform);
        skillSlot.GetComponent<SkillSlot>().InputSkill(_skill, _iskill);
    }
}