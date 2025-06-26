using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treant : Monster
{
    public override void Init()
    {
        CharacterName = gameObject.name.Replace("(Clone)", "").Trim();
        CharacterType = (int)CHARACTER_TYPE.MONSTER;
        Id = (int)MONSTER_ID.TREANT;
        player = Shared.GameMgr.PLAYEROBJ;
        transform.SetParent(Shared.GameMgr.Monsters.transform, true);
        Fsm_Init();
        Shared.ParticleMgr.CreateParticle("Smoke", transform, 1.0f);
    }

    public override void Fsm_Init()
    {
        Fsm = new StateMachine(new Treant_ChaseState(this));
        CurrState = (int)TREANT_STATE.CHASE;
        PrevState = CurrState;

        DicState.Add((int)TREANT_STATE.CHASE, new Treant_ChaseState(this));
        DicState.Add((int)TREANT_STATE.ATTACK, new Treant_AttackState(this));
        DicState.Add((int)TREANT_STATE.DIE, new Treant_DieState(this));

        Fsm.ChangeState(DicState[(int)TREANT_STATE.CHASE]);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    //float dist = Vector3.Distance(transform.position, Target.transform.position);
    //    //Debug.Log(dist);
    //    Gizmos.DrawWireSphere(transform.position, detectionRange);
    //
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}
}