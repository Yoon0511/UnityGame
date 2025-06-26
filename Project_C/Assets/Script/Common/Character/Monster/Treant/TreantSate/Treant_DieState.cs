using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Treant_DieState : StateBase
{
    Treant Treant;
    float ElapsedTime;
    float DestroyTime;
    public Treant_DieState(Treant _treant)
    {
        Treant = _treant;
    }
    public override void OnStateEnter()
    {
        Treant.PlayAnimation("Ani_State", (int)TREANT_ANI_STATE.DIE);
        DestroyTime = 5.0f;
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        ElapsedTime += Time.deltaTime;

        if(ElapsedTime >= DestroyTime)
        {
            ElapsedTime = 0.0f;
            PhotonNetwork.Destroy(Treant.gameObject);
        }
    }
}
