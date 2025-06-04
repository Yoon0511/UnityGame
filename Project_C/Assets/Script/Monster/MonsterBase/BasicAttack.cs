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
            DamageData damgedata = Shared.GameMgr.DamageDataPool.Get(monster.GetInStatData(STAT_TYPE.ATK), DAMAGEFONT_TYPE.YELLOW);
            other.gameObject.GetComponent<Character>().Hit(damgedata);
            Shared.ParticleMgr.CreateParticle("WhiteHit", Shared.GameMgr.PLAYER.GetBodyParticlePointObj().transform, 0.5f);
        }
    }
}
