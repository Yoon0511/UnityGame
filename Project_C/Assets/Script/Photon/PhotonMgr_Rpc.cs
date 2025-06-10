using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public partial class PhotonMgr : MonoBehaviourPunCallbacks
{
    [PunRPC]
    void OnSkill(bool _Owner,int _Skill)
    {

    }

    public void SendSkill()
    {
        PV.RPC("OnSkill", RpcTarget.All, true, 1);
    }
}
