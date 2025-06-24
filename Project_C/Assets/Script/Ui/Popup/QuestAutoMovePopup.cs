using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAutoMovePopup : MonoBehaviour
{
    int PathId = 0;
    public void Init(int _id)
    {
        PathId = _id;
    }
    public void OnAgree()
    {
        Shared.GameMgr.PLAYER.OnAutoMove(PathId);
        Destroy(gameObject);
    }

    public void OnRefuse()
    {
        Destroy(gameObject);
    }
}
