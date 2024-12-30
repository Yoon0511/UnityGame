using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp : MonoBehaviour
{
    public List<Skill> skillList = new List<Skill>();
    public SkillBook skillbook;
    // Start is called before the first frame update
    void Start()
    {
        //skillslot.InputSkill();
        foreach (Skill skill in skillList)
        {
            skillbook.AddSkill(skill);
        }
    }
}
