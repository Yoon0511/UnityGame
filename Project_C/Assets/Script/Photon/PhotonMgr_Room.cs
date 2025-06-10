using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public partial class PhotonMgr : MonoBehaviourPunCallbacks
{
    public void CreateLobbyRoom(string _strRoom = null)
    {
        if (null == _strRoom)
            return;

        PhotonNetwork.CreateRoom(_strRoom);
    }

    public void RandomLobbyRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinLobbyRoom(string _strRoom = null)
    {
        if (null == _strRoom)
            return;

        PhotonNetwork.JoinRoom(_strRoom);
    }

    public void LeaveRoom(bool _Com = true)
    {
        PhotonNetwork.LeaveRoom(_Com);
    }

    public void SecretLobbyRoom(string _strRoom, byte _Secret, byte _MaxPlayer)
    {
        if (null == _strRoom)
            return;

        bool Open = _Secret > 0 ? false : true;

        RoomOptions roomoption = new RoomOptions() { IsVisible = Open, MaxPlayers = _MaxPlayer };

        if (null == roomoption)
            return;

        PhotonNetwork.JoinOrCreateRoom(_strRoom, roomoption, null);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo room in roomList)
        {
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);

        Debug.Log("·£´ý ·ë ¾øÀ½. »õ·Î¿î ·ë »ý¼º");

        // ·ëÀÌ ¾øÀ¸¸é »ý¼º
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public void SendRoomEntry()
    {
        Player ch = new Player();

        PV.RPC("LobbyRoomEntry", RpcTarget.All, ch);
    }

    public void SendRoomReady()
    {
        PV.RPC("LobbyRoomReady", RpcTarget.Others, true);
    }

    public void SendStartInGame()
    {
        PV.RPC("StartInGame", RpcTarget.All);
    }
}
