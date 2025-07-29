using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MainVirtulCamera : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;

    public void Init()
    {
        VirtualCamera.Follow = Shared.GameMgr.PLAYEROBJ.transform;
        VirtualCamera.LookAt = Shared.GameMgr.PLAYEROBJ.transform;
    }
}
