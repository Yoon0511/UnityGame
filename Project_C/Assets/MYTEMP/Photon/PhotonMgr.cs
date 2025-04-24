using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public partial class PhotonMgr : MonoBehaviourPunCallbacks
{
    public PhotonView PV;

    private void Awake()
    {
        Shared.PhotonMgr = this;

        DontDestroyOnLoad(this);

        PhotonNetwork.GameVersion = "1.0.0";
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 10;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby();

        Debug.Log("OnConnectedToMaster");
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log("OnJoinedLobby");
    }

    public void OnLobby()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
    }

    public void LeaveLobby()
    {
        PhotonNetwork.JoinLobby();
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

    [PunRPC]
    void LobbyRoomEntry(Player _player)
    {

    }

    [PunRPC]
    void LobbyRoomReady(bool _ready)
    {

    }
}
