using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public partial class GameMgr
{
    public Transform SpawnPoint;
    public GameObject PlayerPrefab;

    public GameObject obj;
    public void test()
    {
        Debug.Log("test");
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            Debug.LogWarning("Photon에 연결되지 않았습니다.");
            return;
        }

        if (SpawnPoint == null)
        {
            Debug.LogError("SpawnPoint가 설정되지 않았습니다.");
            return;
        }

        obj = PhotonNetwork.Instantiate("Prefabs/Player", SpawnPoint.position, SpawnPoint.rotation, 0);
        if (obj != null)
            Debug.Log("생성된 오브젝트: " + obj.name);
        else
            Debug.LogError("오브젝트 생성 실패");
    }
}
