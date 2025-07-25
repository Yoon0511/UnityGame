using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using static System.TimeZoneInfo;

public class CineMachineMgr : MonoBehaviour
{
    public GameObject VirtualCamera;
    public CinemachineTargetGroup TargetGroup;

    private void Awake()
    {
        Shared.CineMachineMgr = this;
    }
    public void SetCinemachineTargetGroup(Transform _t1, Transform _t2)
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
        VirtualCamera.SetActive(true);
    }

    public void EndCineMachine()
    {
        VirtualCamera.SetActive(false);
        Shared.MainCamera.ReturnCameraOption();
    }
}
