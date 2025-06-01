using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.U2D;

public class GameMgr : MonoBehaviour
{
    public GameObject PLAYEROBJ;
    public Player PLAYER;

    //UI
    public DamageFont DAMAGE_FONT;
    public Canvas CANVAS;
    public Joystick JOYSTICK;
    public GameObject NPC_DIALOGUEWINDOW;
    NPC_DialogueWindow NPC_DialogueWindow;
    public Hphud Hphud;
    public BuffUi BUFFUI;
    public GameObject CAMERAMOVE;
    //UI
    public DamageDataPool DamageDataPool;

    [NonReorderable]
    Dictionary<string, SpriteAtlas> DicSpriteAtlas = new Dictionary<string, SpriteAtlas>();
    private void Awake()
    {
        Shared.GameMgr = this;
        DamageDataPool = new DamageDataPool();
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
}
