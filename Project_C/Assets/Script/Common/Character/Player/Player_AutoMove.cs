using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
    int PathId = 0;
    bool IsAutoMove = false;
    public void OnAutoMove(int _pathid)
    {
        Debug.Log("asd");
        PathId = _pathid;
        ChangeState((int)STATE.AUTOMOVE);
    }

    public void AutoMovePathInit()
    {
        SetPathNode(Shared.PathNodeMgr.GetPathNode(1));
        PathNodeInit(false);
        int NearIndex = FindNearPathNodeIndex();
        SetPathNodeIndex(FindNearPathNodeIndex());

        //자신부터 가장 가까운 지점까지 표시
        Vector3 p2 = GetPathNodePos(NearIndex);
        Shared.PathNodeMgr.CreatePathVisualizer(transform.position, p2, 5);
        
        //길 찾기 지점끼리 길 표시
        Shared.PathNodeMgr.CreatePathVisualizerAll(PathNodePos, 5, NearIndex);
    }

    int FindNearPathNodeIndex()
    {
        int Nearindex = 0;

        float MinDist = float.MaxValue;
        for (int i = 0; i < PathNodePos.Length; ++i)
        {
            float dist = DistXZ(transform.position,PathNodePos[i]);
            if(dist < MinDist)
            {
                MinDist = dist;
                Nearindex = i;
            }
        }
        return Nearindex;
    }

    public void SetIsAutoMove(bool _isAutoMove)
    {
        IsAutoMove = _isAutoMove;
    }

    public bool GetIsAutoMove()
    {
        return IsAutoMove;
    }

    public void AutoMoveCancle()
    {
        IsAutoMove = false;
        if(IsRiding)
        {
            ChangeState((int)STATE.RIDING);
        }
        else
        {
            ChangeState((int)STATE.IDLE);
        }
        Shared.PathNodeMgr.DeleteAllPathVisualizer();
    }
}
