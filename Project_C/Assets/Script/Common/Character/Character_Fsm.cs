 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract partial class Character : Object
{
    [SerializeField]
    protected float DetectionRange;
    [SerializeField]
    protected float AttackRange;
    protected GameObject Target;

    protected StateMachine Fsm = new StateMachine();
    protected int CurrState;
    protected int PrevState;
    protected Dictionary<int, StateBase> DicState = new Dictionary<int, StateBase>();
    public abstract void Fsm_Init();

    public void ChangeState(int _state)
    {
        if (CurrState == _state)
            return;

        PrevState = CurrState;
        CurrState = _state;

        ChangeFsm();
    }

    public void ChangeState(int _state,int _anistate)
    {
        if (CurrState == _state)
            return;
        PrevState = CurrState;
        CurrState = _state;

        PlayAnimation("Ani_State", _anistate);

        ChangeFsm();
    }

    public void ChangeFsm()
    {
        Fsm.ChangeState(DicState[CurrState]);
    }

    public void StateUpdate()
    {
        Fsm.UpdateState();
    }

    public int GetCurrState() { return CurrState; }
    public int GetPrevState() { return PrevState; }

    public void ChangeTarget(GameObject _target)
    {
        Target = _target;
        TargetCharacter = _target.GetComponent<Character>();
    }

    public void MoveToTarget()
    {
        float speed = Statdata.GetData(STAT_TYPE.SPEED);
        Vector3 dir = Target.transform.position - transform.position;
        dir.y = 0f;

        Vector3 MovePos = dir.normalized * speed * Time.deltaTime;
        transform.Translate(MovePos, Space.World);

        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
    }
}
