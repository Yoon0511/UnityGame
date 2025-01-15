using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Dragon : Monster
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("1");
        }
    }
    //private void FixedUpdate()
    //{
    //    Fsm.UpdateState();
    //}
    
    public override void Fsm_Init()
    {
        Debug.Log("Dragon_fsm_init");
    }

    //public override void Hit(float _damage)
    //{

    //}

    public override void Init()
    {
        Debug.Log("Dragon_init");
        Fsm_Init();
    }
}
