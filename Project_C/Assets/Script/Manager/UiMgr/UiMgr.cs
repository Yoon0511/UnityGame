using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiMgr : MonoBehaviour
{
    public GameObject WORLDMAPUI;
    public Store STORE;
    public BuffUi BuffUi;
    public UnitFrame UnitFrame;
    public Inventory Inventory;
    public QuestListUi QuestListUi;
    public MainCamera MainCamera;
    public MiniMap MiniMap;
    public WorldMap WorldMap;
    public SkillBook SkillBook;
    public PartyFrame PartyFrame;
    public EquipmentWindow EquipmentWindow;
    public InfoPlayer InfoPlayer;
    public RespwanPopup RespwanPopup;
    public SoundOptionPopup SoundOptionPopup;
    public GameObject GameClosePopup;

    public List<GameObject> ListOpenUi = new List<GameObject>();
    bool IsOpenUi;
    /// <test>
    public Text Text;
    /// </test>
    private void Awake()
    {
        Shared.UiMgr = this;
    }

    private void FixedUpdate()
    {
        IsOpenUi = false;
        for (int i = 0;i< ListOpenUi.Count;++i)
        {
            if(ListOpenUi[i] != null)
            {
                if (ListOpenUi[i].activeInHierarchy)
                {
                    IsOpenUi = true;
                }
            }
        }
    }
    public bool GetIsOpenUi() { return IsOpenUi; }
    public void CreateSystemMsg(string _msg,SYSTEM_MSG_TYPE _system_msg_type)
    {
        SystemMsg Msg = Shared.PoolMgr.GetObject("SystemMsg").GetComponent<SystemMsg>();
        Msg.Init(_msg, _system_msg_type);
    }

    public void CreateSpeiclMark(string _markname, float _duration, Transform _owner)
    {
        SpecialMark mark = Shared.PoolMgr.GetObject("SpecialMark").GetComponent<SpecialMark>();
        mark.Init(_markname,_duration,_owner);
    }

    public void CreateSelectUi(int _inviteid,Player _player)
    {
        SelectUi selectUi = Shared.PoolMgr.GetObject("SelectUi").GetComponent<SelectUi>();
        selectUi.Init(_inviteid,_player);
    }
    
    public void CreatePartyInvitePopup(Player _player)
    {
        PartyInvitePopup partyInvitePopup = Shared.PoolMgr.GetObject("PartyInvitePopup").GetComponent<PartyInvitePopup>();
        partyInvitePopup.Init(_player);
    }
}
