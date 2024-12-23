using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject punchAttackCollider;

    public void PlayAnimation(int _state)
    {
        animator.SetInteger("Ani_State", _state);
    }

    public void PlayAnimation(MONSTER_STATE _state)
    {
        animator.SetInteger("Ani_State", (int)_state);
    }

    public void OnPunchAttackCollider()
    {
        punchAttackCollider.SetActive(true);
    }

    public void OffPunchAttackCollider()
    {
        punchAttackCollider.SetActive(false);
    }
}
