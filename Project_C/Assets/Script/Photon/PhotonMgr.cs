using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

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

        Debug.Log("���� ����");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Photon ������ ������ �����");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("�� ���� �Ϸ�!");

        Shared.GameMgr.test();
    }

    public void OnLobby()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }
}
