using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    public GameObject PREFAB_SKILLSLOT;

    [SerializeField]
    GameObject ParentContent;

    //private void Start()
    //{
    //    Player player = Shared.GameMgr.PLAYER;
    //    List<Skill> skillList = player.GetSkillList();
    //    foreach (Skill skill in skillList)
    //    {
    //        AddSkill(skill);
    //    }
    //}

    public void Init(Player _player)
    {
        Player player = _player;
        List<Skill> skillList = player.GetSkillList();
        foreach (Skill skill in skillList)
        {
            AddSkill(skill);
        }
    }
    public void AddSkill(Skill _skill)
    {
        GameObject skillSlot = Instantiate(PREFAB_SKILLSLOT, ParentContent.transform);
        skillSlot.GetComponent<SkillSlot>().InputSkill(_skill);
    }
}