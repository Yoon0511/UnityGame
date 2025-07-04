using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class DrawPetItem : MonoBehaviour
{
    public Image DrawItem;
    public Image PetImage;
    public Text GradeText;
    public ParticleSystem Arua;
    public Animator Animator;
    public Image FlashImage;
    public ParticleSystem OpenFlash;

    ITEM_GRADE Grade;
    Color GradeColor;
    string StrGradeColor;
    bool IsOpen = false;
    int PetItemId;
    public void Init(ITEM_GRADE _grade,int _petItemId)
    {
        PetItemId = _petItemId;
        Grade = _grade;
        StrGradeColor = GetGradeColor();
        ColorUtility.TryParseHtmlString(GetGradeColor(), out GradeColor);
    }

    public void OnOpenDrawPetItem()
    {
        Animator.SetInteger("State", (int)DRAWPETITEM_ANI_STATE.EXIT);
        IsOpen = true;
        ChageAruaColor(GradeColor);
        StartCoroutine(IOpenPetItem());
        SupplyItem();
    }

    void SupplyItem()
    {
        Shared.GameMgr.PLAYER.AddItem(PetItemId);
    }


    public void OnEnterDrawItem()
    {
        if(IsOpen)
        {
            return;
        }

        ChageAruaColor(GradeColor);
        Animator.SetInteger("State", (int)DRAWPETITEM_ANI_STATE.ENTER);
    }

    public void OnExitDrawItem()
    {
        if (IsOpen)
        {
            return;
        }

        ChageAruaColor(Color.white);
        Animator.SetInteger("State", (int)DRAWPETITEM_ANI_STATE.EXIT);
    }

    IEnumerator IOpenPetItem()
    {
        FlashImage.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.1f);
        FlashImage.color = new Color(1, 1, 1, 0);

        var main = OpenFlash.main;
        main.startColor = GradeColor;
        OpenFlash.Play();

        DrawItem.color = new Color(1, 1, 1, 0.7f);

        PetImage.gameObject.SetActive(true);
        GradeText.gameObject.SetActive(true);

        GradeText.text = $"<color={StrGradeColor}><b>{Grade}</b></color>";
        PetImage.sprite = Shared.GameMgr.GetSpriteAtlas("Items", "PetItem");
    }

    void ChageAruaColor(Color _color)
    {
        var main = Arua.main;
        main.startColor = _color;

        Arua.Clear();
        Arua.Play();
    }

    string GetGradeColor()
    {
        string color = null;

        switch(Grade)
        {
            case ITEM_GRADE.COMMON:
                color = "#C0C0C0";
                break;
            case ITEM_GRADE.UNCOMMON:
                color = "#1EFF00";
                break;
            case ITEM_GRADE.RARE:
                color = "#0070FF";
                break;
            case ITEM_GRADE.EPIC:
                color = "#A335EE";
                break;
            case ITEM_GRADE.LEGENDARY:
                color = "#FF8000";
                break;
        }

        return color;
    }
}
