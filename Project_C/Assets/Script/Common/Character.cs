using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//plaer - Character - object

public abstract class Character : MonoBehaviour
{
    public StatData statdata;
    protected string character_name;

    //protected STATE curr_state;
    //protected STATE prev_state;
    private void Start()
    {
        Init();
    }
    public abstract void Init();
    public abstract void Hit(float _damage);

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
    public string GetCharacterName() { return character_name; }
    public float GetInStatData(STAT_TYPE _type)
    { 
        return statdata.GetData(_type); 
    }

    public StatData GetStatData()
    {
        return statdata;
    }

    public void EhanceStat()
    {

    }
}