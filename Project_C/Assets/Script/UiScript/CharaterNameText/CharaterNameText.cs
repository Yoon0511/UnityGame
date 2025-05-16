using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaterNameText : MonoBehaviour
{
    public Character Character;
    public float Distance;
    public Transform TextPos;
    public string HexColor; 
    Transform Ctransform;

    GameObject NameText;
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
        NameText.transform.SetParent(Shared.GameMgr.CANVAS.transform, false);
        NameText.transform.SetAsFirstSibling();
        NameText.GetComponent<Text>().text = "<color=#" + HexColor + "><b>" + Character.GetCharacterName() + "</b></color>";;
        OnName();
    }
    public void OnName()
    {
        OnText = true;
        if (NameText == null) return;

        NameText.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(TextPos.position);
    }

    public void OffName()
    {
        OnText = false;
        if(NameText != null)
        {
            NameText.GetComponent<PoolAble>().ReleaseObject();
            NameText = null;
        }
    }

    public void LateUpdate()
    {
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
}
