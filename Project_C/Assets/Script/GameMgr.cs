using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class GameMgr : MonoBehaviour
{
    public GameObject PLAYEROBJ;
    public Player PLAYER;
    public CreateDamageText CREATE_DAMAGE_TEXT;

    [NonReorderable]
    Dictionary<string, SpriteAtlas> DicSpriteAtlas = new Dictionary<string, SpriteAtlas>();
    private void Awake()
    {
        Shared.GameMgr = this;
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
}
