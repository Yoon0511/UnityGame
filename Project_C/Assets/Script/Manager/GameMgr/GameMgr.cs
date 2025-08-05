using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.U2D;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;

public partial class GameMgr : MonoBehaviourPun
{
    public GameObject PLAYEROBJ;
    public Player PLAYER;
    public Terrain Terrain;
    //UI
    public DamageFont DAMAGE_FONT;
    public Canvas CANVAS;
    public Joystick JOYSTICK;
    public GameObject NPC_DIALOGUEWINDOW;
    NPC_DialogueWindow NPC_DialogueWindow;
    public Hphud Hphud;
    public BuffUi BUFFUI;
    public GameObject CAMERAMOVE;
    public FadeInOut FadeInOut;
    //UI
    public DamageDataPool DamageDataPool;

    [NonReorderable]
    Dictionary<string, SpriteAtlas> DicSpriteAtlas = new Dictionary<string, SpriteAtlas>();

    [SerializeField]
    List<Monster> ListMonster = new List<Monster>();
    [SerializeField]
    List<NPC> ListNPC = new List<NPC>();
    [SerializeField]
    List<Player> ListPlayer = new List<Player>();
    List<QuestBase> ListQuest = new List<QuestBase>();

    public Transform SpwanPoint;
    public Transform RespwanPoint;
    public GameObject Monsters;
    private void Awake()
    {
        Shared.GameMgr = this;
        DamageDataPool = new DamageDataPool();
    }

    private void Start()
    {
        Shared.PhotonMgr.SpawnPoint = SpwanPoint;
        Shared.PhotonMgr.JoinRoom();
        SavePathInit();
        //시네머신 설정
        Shared.CineMachineMgr.EndCineMachine();
    }

    public Sprite GetSpriteAtlas(string _Atlas, string _Name)
    {
        if (DicSpriteAtlas.ContainsKey(_Atlas))
            return DicSpriteAtlas[_Atlas].GetSprite(_Name);

        object obj = null;

        obj = Resources.Load("Atlas/" + _Atlas);

        if (obj == null)
            return null;

        SpriteAtlas sa = obj as SpriteAtlas;

        if (sa != null)
        {
            DicSpriteAtlas.Add(_Atlas, sa);

            return sa.GetSprite(_Name);
        }
        return null;
    }

    public bool IsJoystickDrag()
    {
        return JOYSTICK.IsDrag;
    }

    public void OnNPCDialogueWindow(NPC _npc,Player _player)
    {
        if (NPC_DialogueWindow == null)
            NPC_DialogueWindow = NPC_DIALOGUEWINDOW.GetComponent<NPC_DialogueWindow>();

        NPC_DIALOGUEWINDOW.SetActive(true);
        NPC_DialogueWindow.Init(_npc,_player);
    }

    public bool IsCheckCharacterType(Collider _other,int _type)
    {
        Character charcter;
        if (_other.TryGetComponent<Character>(out charcter))
        {
            int Ctype = charcter.GetCharacterType();
            return Ctype == _type;
        }
        else
        {
            return false;
        }
    }

    public GameObject GetMiddleObj(Vector3 pos1, Vector3 pos2)
    {
        Vector3 Middlepos = (pos1 + pos2) / 2f;

        GameObject hitpoint = new GameObject();
        hitpoint.transform.position = Middlepos;
        hitpoint.transform.rotation = Quaternion.identity;

        return hitpoint;
    }

    public void AddNPC(Character _npc)
    {
        NPC npc = _npc as NPC;
        if (npc != null)
        {
            ListNPC.Add(npc);
        }
    }

    public void AddMonster(Character _monster)
    {
        Monster monster = _monster as Monster;
        if (monster != null)
        {
            ListMonster.Add(monster);
        }
    }
    public void AddQuest(QuestBase _quest)
    {
        ListQuest.Add(_quest);
    }

    public void RemoveMonster(Monster _monster)
    {
        ListMonster.Remove(_monster);
        Debug.Log(ListMonster.Count);
    }
    public void AllMonsterSetPlayer()
    {
        for (int i = 0; i < ListMonster.Count;++i)
        {
            ListMonster[i].SetPlayer(PLAYEROBJ);
        }
    }

    public List<Monster> GetListMonster()
    {
        return ListMonster;
    }

    public List<NPC> GetListNPC()
    {
        return ListNPC;
    }

    public List<QuestBase> GetListQuest()
    {
        return ListQuest;
    }

    public NPC GetNPCinList(int _id)
    {
        for(int i =0;i<ListNPC.Count;++i)
        {
            if (ListNPC[i].GetId() == _id)
            {
                return ListNPC[i];
            }
        }
        return null;
    }

    public void AddPlayer(Player _player)
    {
        ListPlayer.Add(_player);
    }
    public List<Player> GetListPLayer()
    {
        return ListPlayer;
    }

    public Player GetPlayerinListforViewid(int _viewid)
    {
        for (int i = 0; i < ListPlayer.Count; ++i)
        {
            if (ListPlayer[i].GetPhotonViewId() == _viewid)
            {
                return ListPlayer[i];
            }
        }
        return null;
    }

    public float GetTerrainHeight(Vector3 position)
    {
        return Terrain.activeTerrain.SampleHeight(position);
    }

    public void OnPlayerAttack()
    {
        PLAYER.OnAttack();
    }

    public void OnGameClose()
    {
        Save();
        Application.Quit();
    }
}
