using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public partial class Player
{
    [SerializeField]
    List<Player> Party = new List<Player>();

    int SelectPlayerViewId;
    public void PartyInvite(int _inviteid)
    {
        Shared.UiMgr.CreatePartyInvitePopup(this);
        SelectPlayerViewId = _inviteid;
    }

    public void PartyJoin(Player _member)
    {
        if(Party.Count == 0)
        {
            Party.Add(this);
            if (PV.IsMine)
            {
                Shared.UiMgr.PartyFrame.AddPartyPlayer(this);
            }
        }

        Party.Add(_member);
        if(PV.IsMine)
        {
            Shared.UiMgr.PartyFrame.AddPartyPlayer(_member);
        }

        Shared.UiMgr.CreateSystemMsg($"{GetCharacterName()}님의 파티에 가입하였습니다.", SYSTEM_MSG_TYPE.UI);
    }

    public int GetSelectPlayerViewId()
    {
        return SelectPlayerViewId;
    }

    public void SetSelectPlayerViewId(int _id)
    {
        SelectPlayerViewId = _id;
    }
}
