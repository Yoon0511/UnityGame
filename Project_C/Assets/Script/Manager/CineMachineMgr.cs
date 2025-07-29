using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using static System.TimeZoneInfo;

public class CineMachineMgr : MonoBehaviour
{
    public MainVirtulCamera MainVirtulCamera;
    public GameObject TalkCamera;
    public CinemachineTargetGroup TargetGroup;

    private void Awake()
    {
        Shared.CineMachineMgr = this;
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
    }
}
