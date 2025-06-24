using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster
{
    [SerializeField]
    GameObject BasicAttackCollider;

    public void OnBasicAttackCollider()
    {
        BasicAttackCollider.SetActive(true);
        Shared.SoundMgr.PlaySFX("GOLEM_ATK");
    }

    public void OffBasicAttackCollider()
    {
        BasicAttackCollider.SetActive(false);
    }
}
