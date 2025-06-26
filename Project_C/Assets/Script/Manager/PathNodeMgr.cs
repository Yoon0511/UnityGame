using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.Cockpit;
using UnityEngine;

public class PathNodeMgr : MonoBehaviour
{
    Dictionary<int, Transform[]> DicPathNode = new Dictionary<int, Transform[]>();

    public Transform[] PathNodes;

    public GameObject PathVisualizers;
    public GameObject PathVisualizerPrefab;

    List<GameObject> ListPathVisualizer = new();
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

    public void CreatePathVisualizer(Vector3 _p1, Vector3 _p2,int _count)
    {
        Vector3 LineDir = _p2 - _p1;
        float Dist = Vector3.Distance(_p2, _p1);
        float Offset= Dist/_count;

        for(int i = 0;i<_count;++i)
        {
            GameObject obj = Instantiate(PathVisualizerPrefab);
            Vector3 pos = _p1 + LineDir.normalized * Offset * i;
            pos.y = Shared.GameMgr.GetTerrainHeight(pos) + 0.1f;
            obj.transform.position = pos;
            obj.transform.SetParent(PathVisualizers.transform, true);
            ListPathVisualizer.Add(obj);
        }
    }

    public void CreatePathVisualizerAll(Vector3[] _path, int _count,int _nearIndex)
    {
        for(int i = _nearIndex; i<_path.Length-1;++i)
        {
            CreatePathVisualizer(_path[i], _path[i+1], _count);
        }
    }


    public void DeleteAllPathVisualizer()
    {
        for (int i = ListPathVisualizer.Count - 1; i >= 0; i--)
        {
            if (ListPathVisualizer[i] != null)
                Destroy(ListPathVisualizer[i]);
        }
        ListPathVisualizer.Clear();
    }
}
