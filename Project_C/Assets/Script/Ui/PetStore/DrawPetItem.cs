using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawPetItem : MonoBehaviour
{
    public Image Image;
    ITEM_GRADE Grade;
    Color GradeColor;
    public void Init(ITEM_GRADE _grade)
    {
        Grade = _grade;
        ColorUtility.TryParseHtmlString(GetGradeColor(), out GradeColor);
    }

    public void OnCheckDrawItem()
    {
        Image.color = GradeColor;
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
