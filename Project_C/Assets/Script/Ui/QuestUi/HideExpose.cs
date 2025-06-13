using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideExpose : MonoBehaviour
{
    public Image Image;

    public Animator Animator;
    bool Hide = true;
    public void OnBtnClick()
    {
        if(Hide)
        {
            //º˚±Ë(√÷º“»≠)
            Animator.SetInteger("Hide", 1);
            Image.sprite = Shared.GameMgr.GetSpriteAtlas("Common", "Enlargement");
        }
        else
        {
            //∫∏¿”(√÷¥Î»≠)
            Animator.SetInteger("Hide", 0);

            Image.sprite = Shared.GameMgr.GetSpriteAtlas("Common", "Reduction");
        }
        Hide = !Hide;
    }
}
