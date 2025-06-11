using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public partial class PhotonMgr : MonoBehaviourPunCallbacks
{
    [PunRPC]
    void OnSkill(int _viewid,int _skillid, int _index)
    {
        Player player = Shared.GameMgr.GetPlayerinListforViewid(_viewid);

        player.SetCurrentSkill(_index, _skillid);
        player.UseOtherPlayerSkill(_index);
    }

    public void SendSkill(int _viewid, int _skillid, int _index)
    {
        PV.RPC("OnSkill", RpcTarget.Others, _viewid, _skillid, _index);
    }
}
