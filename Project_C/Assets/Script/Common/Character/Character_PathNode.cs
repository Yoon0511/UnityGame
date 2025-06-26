using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract partial class Character : Object
{
    [SerializeField]
    protected Transform[] PathNode;
    protected Vector3[] PathNodePos;
    protected int PathNodeIndex = 0;

    bool LoopPath = false;
    bool IsPathComplete = false;
    public void PathNodeInit(bool _loopPath = true)
    {
        PathNodeIndex = 0;
        LoopPath = _loopPath;
        IsPathComplete = false;
        PathNodePosInit();
    }
    public void PathNodePosInit()
    {
        PathNodePos = new Vector3[PathNode.Length];
        for (int i = 0; i < PathNode.Length; i++)
        {
            PathNodePos[i] = PathNode[i].position;
        }
    }
    public void PathNodeUpdate()
    {
        if(IsPathComplete)
        {
            return;
        }

        float speed = Statdata.GetData(STAT_TYPE.SPEED);
        Vector3 dir = PathNodePos[PathNodeIndex] - transform.position;
        dir.y = 0f; //y축 거리는 제외

        Vector3 MovePos = dir.normalized * speed * Time.deltaTime;
        transform.Translate(MovePos, Space.World);

        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

        //y축을 제외한 거리 계산
        float dist = DistXZ(transform.position, PathNodePos[PathNodeIndex]);

        if (dist <= 0.5f)
        {
            PathNodeIndex++;
            if (PathNodeIndex >= PathNodePos.Length)
            {
                PathNodeIndex = 0;
                if (LoopPath == false) // 끝 지점에 도착시
                {
                    IsPathComplete = true;
                }
            }
        }
    }

    public float DistXZ(Vector3 _a, Vector3 _b)
    {
        Vector2 a = new Vector2(_a.x, _a.z);
        Vector2 b = new Vector2(_b.x, _b.z);
        float dist = Vector2.Distance(a, b);
        return dist;
    }

    public void SetPathNodeIndex(int _index)
    {
        PathNodeIndex = _index;
    }

    public void SetPathNode(Transform[] _pathnode)
    {
        PathNode = _pathnode;
    }

    public Vector3 GetPathNodePos(int _index)
    {
        return PathNodePos[PathNodeIndex];
    }
    public bool GetIsPathComplete()
    {
        return IsPathComplete;
    }
}
