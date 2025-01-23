using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp : MonoBehaviour
{
    public Skill testSkill;
    public Character Character;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            testSkill.SetOwner(Character.gameObject);
            testSkill.UseSkill();
        }
    }
}
