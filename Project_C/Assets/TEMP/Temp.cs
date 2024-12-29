using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp : MonoBehaviour
{
    public SkillFireBall FireBall;

    public SkillBtn ui;

    public SkillSlot skillslot;
    // Start is called before the first frame update
    void Start()
    {
        //FireBall.UseSkill();

        ui.InputSkill(FireBall);

        skillslot.InputSkill(FireBall);
    }
}
