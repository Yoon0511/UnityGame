using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI; // UI 요소(Text)에 필요

public partial class PhotonMgr : MonoBehaviourPunCallbacks
{
    // --- 퍼블릭 참조 (인스펙터에서 할당) ---
    public PhotonView PV; // 일반적으로 PhotonView는 동기화하려는 게임 오브젝트에 있어야 합니다.
                          // PhotonMgr 자체가 동기화되는 경우가 아니라면 여기에 직접 두는 것은 흔치 않습니다.
    public Transform SpawnPoint; // 명확성을 위해 철자 수정
    public Text StatusText; // "Test"보다 목적을 더 잘 나타내도록 이름 변경

    // --- 프라이빗 변수 ---
    private const string GameVersion = "1.0.0";
    private const string RoomName = "TestRoom"; // 룸 이름 중앙화
    private const byte MaxPlayersPerRoom = 4;

    // --- Unity 생명 주기 메서드 ---
    private void Awake()
    {
        // 이 인스턴스를 정적 참조에 할당 (예: 싱글톤 패턴)
        // 'Shared' 클래스와 'PhotonMgr' 정적 필드가 존재하는지 확인하세요.
        // 예: public static class Shared { public static PhotonMgr PhotonMgr; }
        if (Shared.PhotonMgr == null)
        {
            Shared.PhotonMgr = this;
            DontDestroyOnLoad(gameObject); // 이 스크립트가 있는 게임 오브젝트에 적용
        }
        else if (Shared.PhotonMgr != this)
        {
            // 이미 존재하는 인스턴스가 있다면 중복 제거
            Destroy(gameObject);
            return;
        }

        // Photon 네트워크 설정 초기화
        PhotonNetwork.GameVersion = GameVersion;
        PhotonNetwork.SendRate = 20; // 초당 데이터 전송 빈도
        PhotonNetwork.SerializationRate = 10; // OnPhotonSerializeView 호출 빈도
        //PhotonNetwork.ConnectToRegion("asia");
        // 연결 프로세스 시작
        PhotonNetwork.ConnectUsingSettings();

        UpdateStatusText("서버에 연결 중...");
        Debug.Log("Photon 서버에 연결 시도 중.");

        // 중요 체크: SpawnPoint가 할당되었는지 확인
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
        // 필요하다면 여기에 재연결 로직 구현
        // PhotonNetwork.ConnectUsingSettings(); 
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        UpdateStatusText("Photon 마스터 서버에 연결됨. 로비에 입장 중...");
        Debug.Log("Photon 마스터 서버에 연결됨.");

        // 룸 목록을 얻기 위해 로비에 입장 (UI가 있다면 유용)
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        UpdateStatusText("로비에 입장함. 룸 참여 또는 생성 시도 중...");
        Debug.Log("로비에 입장함.");

        // 기존 룸에 참여 시도, 없으면 생성
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
        UpdateStatusText("룸 참여 완료! 플레이어 스폰 중...");
        Debug.Log($"룸 참여 완료: {PhotonNetwork.CurrentRoom.Name}");

        // SpawnPoint가 유효할 때만 플레이어 인스턴스화
        if (SpawnPoint != null)
        {
            // 네트워크를 통해 플레이어 프리팹 인스턴스화
            PhotonNetwork.Instantiate("Prefabs/Player", SpawnPoint.position, SpawnPoint.rotation, 0);
        }
        else
        {
            Debug.LogError("플레이어를 스폰할 수 없습니다: SpawnPoint가 null입니다.");
            UpdateStatusText("오류: 플레이어 스폰 실패 (SpawnPoint 누락).");
        }

        //마스터 클라이언트라면 몬스터 생성
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
        // 여기에 새로운 룸을 생성하거나 로비에 다시 참여하는 로직을 추가할 수 있습니다.
    }

    // 참고: OnJoinRandomFailed는 PhotonNetwork.JoinRandomRoom()을 사용할 때만 호출됩니다.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        UpdateStatusText($"임의 룸 참여 실패: {message}. 새 룸을 생성 중...");
        Debug.LogWarning($"임의 룸 참여 실패 (코드: {returnCode}). 새 룸을 생성 중.");

        RoomOptions roomOptions = new RoomOptions { MaxPlayers = MaxPlayersPerRoom };
        // 고유한 이름으로 새 룸 생성 (null은 Photon이 GUID를 생성함을 의미)
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    // --- 헬퍼 메서드 ---

    /// <summary>
    /// UI 상태 텍스트를 업데이트하고 메시지를 콘솔에 기록합니다.
    /// </summary>
    /// <param name="message">표시할 메시지입니다.</param>
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

    /// <summary>
    /// 현재 룸에 있는 플레이어 수를 확인하고 기록합니다.
    /// </summary>
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

    // --- 퍼블릭 메서드 (UI 상호 작용용으로 사용될 수 있음) ---

    /// <summary>
    /// Photon의 메시지 큐 처리를 활성화합니다. 씬 로딩 중처럼 비활성화한 후 유용합니다.
    /// </summary>
    public void EnablePhotonMessageQueue()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        Debug.Log("Photon 메시지 큐 활성화됨.");
    }

    /// <summary>
    /// 현재 Photon 로비를 떠납니다.
    /// </summary>
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

    /// <summary>
    /// 현재 Photon 룸을 떠납니다.
    /// </summary>
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