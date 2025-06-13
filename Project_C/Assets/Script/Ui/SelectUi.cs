using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SelectUi : PoolAble
{
    [SerializeField]
    RectTransform RectTransform;

    Player Player;
    Transform Target;
    private void Awake()
    {
        transform.SetParent(Shared.GameMgr.CANVAS.transform, false);
    }

    private void FixedUpdate()
    {
        if(Player != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(Target.position);
            pos.x += 80f;
            RectTransform.position = pos;
        }
    }
    public void Init(int _inviteid,Player _player)
    {
        Player = _player;
        Target = Player.transform;
    }

    public void OnInviteParty()
    {
        Debug.Log("InveiteParty");
        Shared.PhotonMgr.SendPartyInvite(Player.GetPhotonViewId(),Player.GetSelectPlayerViewId());
        
        Player invitePlayer = Shared.GameMgr.GetPlayerinListforViewid(Player.GetPhotonViewId());
        Shared.UiMgr.CreateSystemMsg($"{invitePlayer.GetCharacterName()}님에게 파티 요청", SYSTEM_MSG_TYPE.UI);
        
        OnExit();
    }

    public void OnExit()
    {
        Player = null;
        ReleaseObject();
    }
}
