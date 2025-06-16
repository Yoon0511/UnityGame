using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterNameText : MonoBehaviour
{
    public Character Character;
    public float Distance;
    public Transform TextPos;
    Transform Ctransform;

    GameObject NameText;
    CharacterNameTextUi CharacterNameTextUi;
    GameObject PlayerObj;

    bool OnText = false;

    private void Start()
    {
        Ctransform = Character.transform;
        PlayerObj = Shared.GameMgr.PLAYEROBJ;
    }

    public void CreateName()
    {
        OnText = true;
        NameText = Shared.PoolMgr.GetObject("CharaterNameText");

        CharacterNameTextUi = NameText.GetComponent<CharacterNameTextUi>();
        CharacterNameTextUi.Init(Character);

        NameText.transform.SetParent(Shared.GameMgr.CANVAS.transform, false);
        NameText.transform.SetAsFirstSibling();
        NameText.GetComponent<Text>().text = Character.GetCharacterName();
        OnName();
    }
    public void OnName()
    {
        OnText = true;
        if (NameText == null) return;

        NameText.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(TextPos.position);
        CharacterNameTextUi.MarkUpdate();
    }

    public void OffName()
    {
        OnText = false;
        if(NameText != null)
        {
            NameText.GetComponent<PoolAble>().ReleaseObject();
            NameText = null;
            CharacterNameTextUi.MarkUpdate();
        }
    }

    public void LateUpdate()
    {
        if (PlayerObj == null)
        {
            PlayerObj = Shared.GameMgr.PLAYEROBJ;
            return;
        }
        float dist = Vector3.Distance(Ctransform.position, PlayerObj.transform.position);

        if (dist < Distance) //일정 거리 이내이면 이름 표시
        {
            if(NameText == null)
            {
                CreateName();
            }
            else
            {
                OnName();
            }
        }
        else
        {
            if (OnText == true)
            {
                OffName();
            }
        }
    }

    public CharacterNameTextUi GetSpecialMark()
    {
        return CharacterNameTextUi;
    }
}
