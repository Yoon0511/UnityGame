using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Monster
{
    [SerializeField]
    bool IsLeader;

    public List<Wolf> ListColleagueMonsters = new List<Wolf>();
    public override void Init()
    {
        CharacterName = gameObject.name;
        CharacterType = (int)CHARACTER_TYPE.MONSTER;
        Id = (int)MONSTER_ID.WOLF;
        player = Shared.GameMgr.PLAYEROBJ;
        Fsm_Init();

        if(IsLeader)
        {
            ListColleagueMonsters.Add(this);

            for (int i = 0; i < ListColleagueMonsters.Count - 1; ++i)
            {
                ListColleagueMonsters[i].ListColleagueMonsters = ListColleagueMonsters;
            }
        }
    }

    public override void Hit(DamageData _damagedata)
    {
        base.Hit(_damagedata);

        //동료에게 알림
        if(GetCurrState() != (int)MONSTER_STATE.ATTACK ||
            GetCurrState() != (int)MONSTER_STATE.CHASE)
        {
            for (int i = 0; i < ListColleagueMonsters.Count; ++i)
            {
                ListColleagueMonsters[i].ChangeTarget(Shared.GameMgr.PLAYEROBJ);
                ListColleagueMonsters[i].ChangeState((int)MONSTER_STATE.CHASE);
            }
        }
    }
    public override void Fsm_Init()
    {
        Fsm = new StateMachine(new Monster_IdleState(this));
        CurrState = (int)MONSTER_STATE.IDLE;
        PrevState = CurrState;

        DicState.Add((int)MONSTER_STATE.IDLE, new Monster_IdleState(this));
        DicState.Add((int)MONSTER_STATE.MOVE, new Monster_MoveState(this));
        DicState.Add((int)MONSTER_STATE.PATROL, new Monster_PatrolState(this));
        DicState.Add((int)MONSTER_STATE.CHASE, new Wolf_ChaseState(this));
        DicState.Add((int)MONSTER_STATE.ATTACK, new Monster_AttackState(this));
        DicState.Add((int)MONSTER_STATE.DIE, new Monster_DieState(this));

        Fsm.ChangeState(DicState[(int)MONSTER_STATE.IDLE]);
    }

    public void ChangeStateColleaue(int _state)
    {
        for (int i = 0; i < ListColleagueMonsters.Count; ++i)
        {
            ListColleagueMonsters[i].ChangeTarget(Shared.GameMgr.PLAYEROBJ);
            ListColleagueMonsters[i].ChangeState((int)MONSTER_STATE.CHASE);
        }
    }
    public bool GetIsLeader() { return IsLeader; }
    public void SetIsLeader(bool _value) { IsLeader = _value; }
}
