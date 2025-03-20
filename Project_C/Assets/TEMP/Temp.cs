using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp : MonoBehaviour
{
    public Skill testSkill;
    public Character Character;
    
    public AtkRange range;
    bool temp = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            testSkill.SetOwner(Character.gameObject);
            testSkill.UseSkill();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            temp = !temp;
            range.gameObject.SetActive(temp);
        }
    }
}
