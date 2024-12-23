using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//plaer - Character - object

public abstract class Character : MonoBehaviour
{
    protected float maxhp;
    protected float hp;
    protected float maxmp;
    protected float mp;
    protected float atk;
    protected float def;
    protected float speed;

    //protected STATE curr_state;
    //protected STATE prev_state;
    private void Start()
    {
        Init();
    }
    public abstract void Init();
    public abstract void Hit(float damage);

    /*
    public void ChangeState(STATE _newstate)
    {
        if (curr_state == _newstate)
            return;

        prev_state = curr_state;
        curr_state = _newstate;
    }
    public STATE GetCurrState()
    {
        return curr_state;
    }

    public STATE GetPrevState()
    {
        return prev_state;
    }
    */

    public float GetAtk() { return atk; }
}