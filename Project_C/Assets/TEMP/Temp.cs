using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp : MonoBehaviour
{
    public Skill_Breath breath;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            breath.UseSkill();
        }
    }
}
