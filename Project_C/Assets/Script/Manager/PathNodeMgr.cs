using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNodeMgr : MonoBehaviour
{
    Dictionary<int, Transform[]> DicPathNode = new Dictionary<int, Transform[]>();

    public Transform[] PathNodes;

    private void Awake()
    {
        Shared.PathNodeMgr = this;
    }
    private void Start()
    {
        DicPathNode.Add(1, PathNodes);
    }

    public Transform[] GetPathNode(int _id)
    {
        return DicPathNode[_id];
    }
}
