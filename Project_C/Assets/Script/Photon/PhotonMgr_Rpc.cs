using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    [PunRPC]
    void CreateAtkRange(Vector3[] _positions, float[] _photonTime)
    {
        for(int i = 0; i < _positions.Length; ++i)
        {
            GameObject AtkCircle = Shared.PoolMgr.GetObject("AtkRange_Circle");
            AtkCircle.transform.position = _positions[i];
            AtkCircle.GetComponent<AtkRange>().Init(true);
            AtkCircle.GetComponent<AtkRange>().SetDesiredTime(_photonTime[i]);
            //GameObject AtkCircle = Instantiate(AtkRangeCircle, _positions[i], Quaternion.identity);
        }
    }
    public void SendAtkRange(Vector3[] _positions, float[] _photonTime)
    {
        PV.RPC("CreateAtkRange", RpcTarget.Others, _positions, _photonTime);
    }

    //파티초대
    [PunRPC]
    void PartyInvite(int _inviteid,int _beinviteid)
    {
        Player player = Shared.GameMgr.GetPlayerinListforViewid(_beinviteid);

        if (player != null)
        {
            player.PartyInvite(_inviteid);
        }
    }
    public void SendPartyInvite(int _inviteid,int _beinviteid)
    {
        PV.RPC("PartyInvite", RpcTarget.Others, _inviteid,_beinviteid);
    }

    //파티가입 수락
    [PunRPC]
    void PartyAccept(int _inviteid, int _beinviteid)
    {
        Player inviteplayer = Shared.GameMgr.GetPlayerinListforViewid(_inviteid);
        Player beinviteplayer = Shared.GameMgr.GetPlayerinListforViewid(_beinviteid);

        if (inviteplayer != null)
        {
            inviteplayer.PartyJoin(beinviteplayer);
            beinviteplayer.PartyJoin(inviteplayer);
        }
    }
    public void SendPartyAccept(int _inviteid,int _beinviteid)
    {
        PV.RPC("PartyAccept", RpcTarget.All,_inviteid,_beinviteid);
    }

    //파티가입 거절
    [PunRPC]
    void PartyRefuse()
    {

    }
    public void SendPartyRefuse()
    {
        PV.RPC("PartyRefuse", RpcTarget.Others);
    }
}
