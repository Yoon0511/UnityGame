using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using static System.TimeZoneInfo;

public class CineMachineMgr : MonoBehaviour
{
    public MainVirtulCamera MainVirtulCamera;
    public GameObject TalkCamera;
    public GameObject TalkCameraParent;
    public CinemachineTargetGroup TargetGroup;

    private void Awake()
    {
        Shared.CineMachineMgr = this;
    }

    public void CinemachineCameraInit()
    {
        //���� ī�޶� �ʱ�ȭ
        MainVirtulCamera.Init();

        //TalkCamera �ʱ�ȭ
        TalkCameraParent.transform.SetParent(Shared.GameMgr.PLAYEROBJ.transform);

        TalkCamera.GetComponent<CinemachineVirtualCamera>().Priority = 3;
        TargetGroup.m_Targets = new CinemachineTargetGroup.Target[]
        {
            new CinemachineTargetGroup.Target { target = MainVirtulCamera.VirtualCamera.Follow, weight = 1, radius = 1 }
        };
    }


    public void StartTalk(Transform _t1, Transform _t2)
    {
        StartCineMachine();

        TargetGroup.m_Targets = new CinemachineTargetGroup.Target[]
        {
            new CinemachineTargetGroup.Target { target = _t1, weight = 1, radius = 1 },
            new CinemachineTargetGroup.Target { target = _t2, weight = 1, radius = 1 }
        };
    }

    public void StartCineMachine()
    {
        TalkCamera.GetComponent<CinemachineVirtualCamera>().Priority = 15;
    }

    public void EndCineMachine()
    {
        TalkCamera.GetComponent<CinemachineVirtualCamera>().Priority = 3;

        TargetGroup.m_Targets = new CinemachineTargetGroup.Target[]
        {
            new CinemachineTargetGroup.Target { target = MainVirtulCamera.VirtualCamera.Follow, weight = 1, radius = 1 }
        };
    }
}
