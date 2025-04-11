using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    [SerializeField]
    Monster monster;
    public void OnTriggerEnter(Collider other)
    {
        bool IsPlayer = Shared.GameMgr.IsCheckCharacterType(other, (int)CHARACTER_TYPE.PLAYER);
        if(IsPlayer)
        {
            other.gameObject.GetComponent<Character>().Hit(monster.GetInStatData(STAT_TYPE.ATK));
        }
    }
}
