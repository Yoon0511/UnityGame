using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyFrame : MonoBehaviour
{
    public GameObject PartyMemberPrefab;
    public void AddPartyPlayer(Player _player)
    {
        PartyMember partyMember =  Shared.PoolMgr.GetObject("PartyMember").GetComponent<PartyMember>();
        partyMember.transform.SetParent(transform, false);
        partyMember.Init(_player);
    }
}
