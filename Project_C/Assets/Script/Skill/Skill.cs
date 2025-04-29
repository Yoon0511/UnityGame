using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    public GameObject Owner; //��ų �����
    protected Character OwnerCharacter;
    public string SpriteName;
    public float CoolTime;
    public string SkillName;
    public float Atk;
    public float UseMp;
    public string Explanation;
    public int SkillMotion;
    public int CurrentState;
    public SKILL_TYPE SkillType;
    public bool Instant; //true = ��ų��� ���� ��û��
    public bool IsAtkRange; //���ݻ�Ÿ�ǥ�ø� ��� ����

    public void SetOwner(GameObject _owner)
    {
        Owner = _owner;
        OwnerCharacter = _owner.GetComponent<Character>();
        CurrentState = (int)SKILL_STATE.READY;
    }
    public virtual void UseSkill()
    {
        CurrentState = (int)SKILL_STATE.RUNNING;
    }

    public virtual void SkillEnd()
    {
        CurrentState = (int)SKILL_STATE.END;
    }

    public int GetCurrentState() { return CurrentState; }
    public void SetCurrentState(int _state) {  CurrentState = _state; }
}