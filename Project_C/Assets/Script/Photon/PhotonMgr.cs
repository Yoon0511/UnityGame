using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public partial class PhotonMgr : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    public Transform SpawnPoint;
    public Text StatusText;

    private const string GameVersion = "1.0.0";
    private const string RoomName = "TestRoom";
    private const byte MaxPlayersPerRoom = 4;

    private void Awake()
    {
        if (Shared.PhotonMgr == null)
        {
            Shared.PhotonMgr = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Shared.PhotonMgr != this)
        {
            Destroy(gameObject);
            return;
        }

        PhotonNetwork.GameVersion = GameVersion;
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 10;
        PhotonNetwork.ConnectUsingSettings();

        UpdateStatusText("������ ���� ��...");
        Debug.Log("Photon ������ ���� �õ� ��.");

        //if (SpawnPoint == null)
        //{
        //    Debug.LogError("����: �ν����Ϳ� SpawnPoint�� �Ҵ���� �ʾҽ��ϴ�! �÷��̾� �ν��Ͻ�ȭ�� ������ ���Դϴ�.");
        //    UpdateStatusText("����: ���� ����Ʈ ����!");
        //}
    }

    // --- Photon �ݹ� ---

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        UpdateStatusText($"Photon ���� ����: {cause}");
        Debug.LogError($"Photon ���� ����: {cause}");

        // PhotonNetwork.ConnectUsingSettings(); 
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        UpdateStatusText("Photon ������ ������ �����. �κ� ���� ��...");
        Debug.Log("Photon ������ ������ �����.");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        UpdateStatusText("�κ� ������. �� ���� �Ǵ� ���� �õ� ��...");
        Debug.Log("�κ� ������.");

        // ���� �뿡 ���� �õ�, ������ ����
        // �ΰ��Ӿ����� ���ɽ� ���
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = MaxPlayersPerRoom };
        PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions, TypedLobby.Default);
    }

    //Ÿ��Ʋ������ �ΰ��Ӿ����� �Ѿ�� ���
    public void JoinRoom()
    {
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = MaxPlayersPerRoom };
        PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions, TypedLobby.Default);
    }   
    public override void OnJoinedRoom()
    {
        EnablePhotonMessageQueue();
        UpdateStatusText("�� ���� �Ϸ�! �÷��̾� ���� ��...");
        Debug.Log($"�� ���� �Ϸ�: {PhotonNetwork.CurrentRoom.Name}");

        if (SpawnPoint != null)
        {
            PhotonNetwork.Instantiate("Prefabs/Player", SpawnPoint.position, SpawnPoint.rotation, 0);
        }
        else
        {
            Debug.LogError("�÷��̾ ������ �� �����ϴ�: SpawnPoint�� null�Դϴ�.");
            UpdateStatusText("����: �÷��̾� ���� ���� (SpawnPoint ����).");
        }

        if (PhotonNetwork.IsMasterClient)
        {
            //���� ����
            Shared.GameMgr.MonsterSpawn();
        }
        else
        {
            //���� ���� ����
            Shared.GameMgr.RemoveLocalMonster();
        }

        //CheckPlayersInRoom();

        //StatusText.text = PhotonNetwork.CloudRegion;
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        //UpdateStatusText($"�÷��̾� {newPlayer.NickName}�� �뿡 �����߽��ϴ�.");
        //Debug.Log($"�÷��̾� {newPlayer.NickName} (ID: {newPlayer.ActorNumber})�� �뿡 �����߽��ϴ�.");
        //CheckPlayersInRoom();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        //UpdateStatusText($"�÷��̾� {otherPlayer.NickName}�� ���� �������ϴ�.");
        //Debug.Log($"�÷��̾� {otherPlayer.NickName} (ID: {otherPlayer.ActorNumber})�� ���� �������ϴ�.");
        //CheckPlayersInRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        UpdateStatusText($"�� ���� ����: {message} (�ڵ�: {returnCode})");
        Debug.LogError($"�� ���� ����: {message} (�ڵ�: {returnCode})");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        UpdateStatusText($"���� �� ���� ����: {message}. �� ���� ���� ��...");
        Debug.LogWarning($"���� �� ���� ���� (�ڵ�: {returnCode}). �� ���� ���� ��.");

        RoomOptions roomOptions = new RoomOptions { MaxPlayers = MaxPlayersPerRoom };
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    private void UpdateStatusText(string message)
    {
        if(StatusText == null)
        {
            return;
        }
            
        if (StatusText != null)
        {
            StatusText.text = message;
        }
        else
        {
            Debug.LogWarning($"StatusText UI ��Ұ� �Ҵ���� �ʾҽ��ϴ�. �޽���: {message}");
        }
    }

    void CheckPlayersInRoom()
    {
        Debug.Log($"Is Master Client: {PhotonNetwork.IsMasterClient}");
        if (PhotonNetwork.InRoom)
        {
            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            UpdateStatusText($"���� �÷��̾�: {playerCount}/{MaxPlayersPerRoom}");
            Debug.Log($"�� '{PhotonNetwork.CurrentRoom.Name}'�� ���� �÷��̾�: {playerCount}");
        }
        else
        {
            UpdateStatusText("�뿡 ���� ���� �ƴ�.");
            Debug.Log("�÷��̾� ���� Ȯ���ϱ� ���� ���� �뿡 ���� ���� �ƴմϴ�.");
        }
    }

    public void EnablePhotonMessageQueue()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        Debug.Log("Photon �޽��� ť Ȱ��ȭ��.");
    }

    public void LeaveCurrentLobby()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
            UpdateStatusText("�κ� ������ ��...");
            Debug.Log("Photon �κ� ������ ��.");
        }
        else
        {
            Debug.Log("���� �κ� ���� ���� �ƴմϴ�.");
        }
    }

    public void LeaveCurrentRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            UpdateStatusText("�� ������ ��...");
            Debug.Log("Photon ���� ������ ��.");
        }
        else
        {
            Debug.Log("���� �뿡 ���� ���� �ƴմϴ�.");
        }
    }
}