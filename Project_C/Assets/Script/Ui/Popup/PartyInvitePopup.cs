using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PartyInvitePopup : PoolAble
{
    [SerializeField]
    RectTransform RectTransform;

    [SerializeField]
    Text text;

    Player Player;

    private void Awake()
    {
        transform.SetParent(Shared.GameMgr.CANVAS.transform, false);
    }
    public void Init(Player _player)
    {
        Player = _player;
        RectTransform.anchoredPosition = Vector2.zero;

        Player player = Shared.GameMgr.GetPlayerinListforViewid(_player.GetPhotonViewId());
        text.text = $"<color=#1E90FF><b>{player.GetCharacterName()}</b></color>님 파티에 참가하시겠습니까?";
    }
    public void OnAccept()
    {
        Shared.SoundMgr.PlaySFX("SUCCESS");
        Shared.PhotonMgr.SendPartyAccept(Player.GetPhotonViewId(), Player.GetSelectPlayerViewId());
        ReleaseObject();
    }

    public void OnRefuse()
    {
        ReleaseObject();
    }
}
