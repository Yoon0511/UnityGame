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
            AtkCircle.GetComponent<AtkRange>().SetDesiredTime(_photonTime[i]);
            //GameObject AtkCircle = Instantiate(AtkRangeCircle, _positions[i], Quaternion.identity);
        }
    }
    public void SendAtkRange(Vector3[] _positions, float[] _photonTime)
    {
        PV.RPC("CreateAtkRange", RpcTarget.Others, _positions, _photonTime);
    }
}
