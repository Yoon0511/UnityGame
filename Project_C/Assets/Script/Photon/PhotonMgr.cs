using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI; // UI ���(Text)�� �ʿ�

public partial class PhotonMgr : MonoBehaviourPunCallbacks
{
    // --- �ۺ� ���� (�ν����Ϳ��� �Ҵ�) ---
    public PhotonView PV; // �Ϲ������� PhotonView�� ����ȭ�Ϸ��� ���� ������Ʈ�� �־�� �մϴ�.
                          // PhotonMgr ��ü�� ����ȭ�Ǵ� ��찡 �ƴ϶�� ���⿡ ���� �δ� ���� ��ġ �ʽ��ϴ�.
    public Transform SpawnPoint; // ��Ȯ���� ���� ö�� ����
    public Text StatusText; // "Test"���� ������ �� �� ��Ÿ������ �̸� ����

    // --- �����̺� ���� ---
    private const string GameVersion = "1.0.0";
    private const string RoomName = "TestRoom"; // �� �̸� �߾�ȭ
    private const byte MaxPlayersPerRoom = 4;

    // --- Unity ���� �ֱ� �޼��� ---
    private void Awake()
    {
        // �� �ν��Ͻ��� ���� ������ �Ҵ� (��: �̱��� ����)
        // 'Shared' Ŭ������ 'PhotonMgr' ���� �ʵ尡 �����ϴ��� Ȯ���ϼ���.
        // ��: public static class Shared { public static PhotonMgr PhotonMgr; }
        if (Shared.PhotonMgr == null)
        {
            Shared.PhotonMgr = this;
            DontDestroyOnLoad(gameObject); // �� ��ũ��Ʈ�� �ִ� ���� ������Ʈ�� ����
        }
        else if (Shared.PhotonMgr != this)
        {
            // �̹� �����ϴ� �ν��Ͻ��� �ִٸ� �ߺ� ����
            Destroy(gameObject);
            return;
        }

        // Photon ��Ʈ��ũ ���� �ʱ�ȭ
        PhotonNetwork.GameVersion = GameVersion;
        PhotonNetwork.SendRate = 20; // �ʴ� ������ ���� ��
        PhotonNetwork.SerializationRate = 10; // OnPhotonSerializeView ȣ�� ��
        //PhotonNetwork.ConnectToRegion("asia");
        // ���� ���μ��� ����
        PhotonNetwork.ConnectUsingSettings();

        UpdateStatusText("������ ���� ��...");
        Debug.Log("Photon ������ ���� �õ� ��.");

        // �߿� üũ: SpawnPoint�� �Ҵ�Ǿ����� Ȯ��
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
        // �ʿ��ϴٸ� ���⿡ �翬�� ���� ����
        // PhotonNetwork.ConnectUsingSettings(); 
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        UpdateStatusText("Photon ������ ������ �����. �κ� ���� ��...");
        Debug.Log("Photon ������ ������ �����.");

        // �� ����� ��� ���� �κ� ���� (UI�� �ִٸ� ����)
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        UpdateStatusText("�κ� ������. �� ���� �Ǵ� ���� �õ� ��...");
        Debug.Log("�κ� ������.");

        // ���� �뿡 ���� �õ�, ������ ����
        //RoomOptions roomOptions = new RoomOptions { MaxPlayers = MaxPlayersPerRoom };
        //PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions, TypedLobby.Default);
    }

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

        // SpawnPoint�� ��ȿ�� ���� �÷��̾� �ν��Ͻ�ȭ
        if (SpawnPoint != null)
        {
            // ��Ʈ��ũ�� ���� �÷��̾� ������ �ν��Ͻ�ȭ
            PhotonNetwork.Instantiate("Prefabs/Player", SpawnPoint.position, SpawnPoint.rotation, 0);
        }
        else
        {
            Debug.LogError("�÷��̾ ������ �� �����ϴ�: SpawnPoint�� null�Դϴ�.");
            UpdateStatusText("����: �÷��̾� ���� ���� (SpawnPoint ����).");
        }

        //������ Ŭ���̾�Ʈ��� ���� ����
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
        // ���⿡ ���ο� ���� �����ϰų� �κ� �ٽ� �����ϴ� ������ �߰��� �� �ֽ��ϴ�.
    }

    // ����: OnJoinRandomFailed�� PhotonNetwork.JoinRandomRoom()�� ����� ���� ȣ��˴ϴ�.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        UpdateStatusText($"���� �� ���� ����: {message}. �� ���� ���� ��...");
        Debug.LogWarning($"���� �� ���� ���� (�ڵ�: {returnCode}). �� ���� ���� ��.");

        RoomOptions roomOptions = new RoomOptions { MaxPlayers = MaxPlayersPerRoom };
        // ������ �̸����� �� �� ���� (null�� Photon�� GUID�� �������� �ǹ�)
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    // --- ���� �޼��� ---

    /// <summary>
    /// UI ���� �ؽ�Ʈ�� ������Ʈ�ϰ� �޽����� �ֿܼ� ����մϴ�.
    /// </summary>
    /// <param name="message">ǥ���� �޽����Դϴ�.</param>
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

    /// <summary>
    /// ���� �뿡 �ִ� �÷��̾� ���� Ȯ���ϰ� ����մϴ�.
    /// </summary>
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

    // --- �ۺ� �޼��� (UI ��ȣ �ۿ������ ���� �� ����) ---

    /// <summary>
    /// Photon�� �޽��� ť ó���� Ȱ��ȭ�մϴ�. �� �ε� ��ó�� ��Ȱ��ȭ�� �� �����մϴ�.
    /// </summary>
    public void EnablePhotonMessageQueue()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        Debug.Log("Photon �޽��� ť Ȱ��ȭ��.");
    }

    /// <summary>
    /// ���� Photon �κ� �����ϴ�.
    /// </summary>
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

    /// <summary>
    /// ���� Photon ���� �����ϴ�.
    /// </summary>
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