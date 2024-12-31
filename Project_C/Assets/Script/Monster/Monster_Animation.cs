using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster
{
    [SerializeField]
    GameObject punchAttackCollider;

    public void OnPunchAttackCollider()
    {
        punchAttackCollider.SetActive(true);
    }

    public void OffPunchAttackCollider()
    {
        punchAttackCollider.SetActive(false);
    }
}
