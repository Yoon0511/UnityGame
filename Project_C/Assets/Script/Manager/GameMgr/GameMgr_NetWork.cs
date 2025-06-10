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
            Debug.LogWarning("Photon�� ������� �ʾҽ��ϴ�.");
            return;
        }

        if (SpawnPoint == null)
        {
            Debug.LogError("SpawnPoint�� �������� �ʾҽ��ϴ�.");
            return;
        }

        obj = PhotonNetwork.Instantiate("Prefabs/Player", SpawnPoint.position, SpawnPoint.rotation, 0);
        if (obj != null)
            Debug.Log("������ ������Ʈ: " + obj.name);
        else
            Debug.LogError("������Ʈ ���� ����");
    }
}
