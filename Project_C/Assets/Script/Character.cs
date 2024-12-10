using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//plaer - Character - object

public abstract class Character : MonoBehaviour
{
    protected float MaxHp;
    protected float Hp;
    protected float MaxMp;
    protected float Mp;
    protected float Atk;
    protected float Def;
    protected float Speed;

    protected STATE Curr_State;
    protected STATE Prev_State;
    private void Start()
    {
        Init();
    }
    public abstract void Init();
    public abstract void Hit(float damage);
    public void ChangeState(STATE _newstate)
    {
        if (Curr_State == _newstate)
            return;

        Prev_State = Curr_State;
        Curr_State = _newstate;
    }
    public STATE GetCurrState()
    {
        return Curr_State;
    }

    public STATE GetPrevState()
    {
        return Prev_State;
    }
}