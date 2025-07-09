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

        UpdateStatusText("서버에 연결 중...");
        Debug.Log("Photon 서버에 연결 시도 중.");

        //if (SpawnPoint == null)
        //{
        //    Debug.LogError("오류: 인스펙터에 SpawnPoint가 할당되지 않았습니다! 플레이어 인스턴스화가 실패할 것입니다.");
        //    UpdateStatusText("오류: 스폰 포인트 누락!");
        //}
    }

    // --- Photon 콜백 ---

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        UpdateStatusText($"Photon 연결 끊김: {cause}");
        Debug.LogError($"Photon 연결 끊김: {cause}");

        // PhotonNetwork.ConnectUsingSettings(); 
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        UpdateStatusText("Photon 마스터 서버에 연결됨. 로비에 입장 중...");
        Debug.Log("Photon 마스터 서버에 연결됨.");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        UpdateStatusText("로비에 입장함. 룸 참여 또는 생성 시도 중...");
        Debug.Log("로비에 입장함.");

        // 기존 룸에 참여 시도, 없으면 생성
        // 인게임씬에서 연걸시 사용
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = MaxPlayersPerRoom };
        PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions, TypedLobby.Default);
    }

    //타이틀씬에서 인게임씬으로 넘어갈때 사용
    public void JoinRoom()
    {
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = MaxPlayersPerRoom };
        PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions, TypedLobby.Default);
    }   
    public override void OnJoinedRoom()
    {
        EnablePhotonMessageQueue();
        UpdateStatusText("룸 참여 완료! 플레이어 스폰 중...");
        Debug.Log($"룸 참여 완료: {PhotonNetwork.CurrentRoom.Name}");

        if (SpawnPoint != null)
        {
            PhotonNetwork.Instantiate("Prefabs/Player", SpawnPoint.position, SpawnPoint.rotation, 0);
        }
        else
        {
            Debug.LogError("플레이어를 스폰할 수 없습니다: SpawnPoint가 null입니다.");
            UpdateStatusText("오류: 플레이어 스폰 실패 (SpawnPoint 누락).");
        }

        if (PhotonNetwork.IsMasterClient)
        {
            //몬스터 생성
            Shared.GameMgr.MonsterSpawn();
        }
        else
        {
            //로컬 몬스터 삭제
            Shared.GameMgr.RemoveLocalMonster();
        }

        //CheckPlayersInRoom();

        //StatusText.text = PhotonNetwork.CloudRegion;
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        //UpdateStatusText($"플레이어 {newPlayer.NickName}이 룸에 입장했습니다.");
        //Debug.Log($"플레이어 {newPlayer.NickName} (ID: {newPlayer.ActorNumber})이 룸에 입장했습니다.");
        //CheckPlayersInRoom();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        //UpdateStatusText($"플레이어 {otherPlayer.NickName}이 룸을 떠났습니다.");
        //Debug.Log($"플레이어 {otherPlayer.NickName} (ID: {otherPlayer.ActorNumber})이 룸을 떠났습니다.");
        //CheckPlayersInRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        UpdateStatusText($"룸 참여 실패: {message} (코드: {returnCode})");
        Debug.LogError($"룸 참여 실패: {message} (코드: {returnCode})");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        UpdateStatusText($"임의 룸 참여 실패: {message}. 새 룸을 생성 중...");
        Debug.LogWarning($"임의 룸 참여 실패 (코드: {returnCode}). 새 룸을 생성 중.");

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
            Debug.LogWarning($"StatusText UI 요소가 할당되지 않았습니다. 메시지: {message}");
        }
    }

    void CheckPlayersInRoom()
    {
        Debug.Log($"Is Master Client: {PhotonNetwork.IsMasterClient}");
        if (PhotonNetwork.InRoom)
        {
            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            UpdateStatusText($"현재 플레이어: {playerCount}/{MaxPlayersPerRoom}");
            Debug.Log($"룸 '{PhotonNetwork.CurrentRoom.Name}'의 현재 플레이어: {playerCount}");
        }
        else
        {
            UpdateStatusText("룸에 참여 중이 아님.");
            Debug.Log("플레이어 수를 확인하기 위해 현재 룸에 참여 중이 아닙니다.");
        }
    }

    public void EnablePhotonMessageQueue()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        Debug.Log("Photon 메시지 큐 활성화됨.");
    }

    public void LeaveCurrentLobby()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
            UpdateStatusText("로비 떠나는 중...");
            Debug.Log("Photon 로비를 떠나는 중.");
        }
        else
        {
            Debug.Log("현재 로비에 참여 중이 아닙니다.");
        }
    }

    public void LeaveCurrentRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            UpdateStatusText("룸 떠나는 중...");
            Debug.Log("Photon 룸을 떠나는 중.");
        }
        else
        {
            Debug.Log("현재 룸에 참여 중이 아닙니다.");
        }
    }
}