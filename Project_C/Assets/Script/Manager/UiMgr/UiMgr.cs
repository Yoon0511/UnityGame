using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiMgr : MonoBehaviour
{
    private void Awake()
    {
        Shared.UiMgr = this;
    }

    public void CreateSystemMsg(string _msg,SYSTEM_MSG_TYPE _system_msg_type)
    {
        SystemMsg Msg = Shared.PoolMgr.GetObject("SystemMsg").GetComponent<SystemMsg>();
        Msg.Init(_msg, _system_msg_type);
    }

    public void CreateSpeiclMark(string _markname, float _duration, Transform _owner)
    {
        SpecialMark mark = Shared.PoolMgr.GetObject("SpecialMark").GetComponent<SpecialMark>();
        mark.Init(_markname,_duration,_owner);
    }
}
