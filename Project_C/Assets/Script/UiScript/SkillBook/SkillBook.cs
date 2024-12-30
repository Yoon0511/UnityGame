using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    public GameObject PREFAB_SKILLSLOT;

    [SerializeField]
    GameObject ParentContent;

    public void AddSkill(Skill skill)
    {
        GameObject skillSlot = Instantiate(PREFAB_SKILLSLOT, ParentContent.transform);
        skillSlot.GetComponent<SkillSlot>().InputSkill(skill);
    }
}
