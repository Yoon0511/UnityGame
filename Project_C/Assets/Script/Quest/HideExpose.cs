using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideExpose : MonoBehaviour
{
    public Animator Animator;
    bool Hide = true;
    public void OnBtnClick()
    {
        if(Hide)
        {
            Animator.SetInteger("Hide", 1);
        }
        else
        {
            Animator.SetInteger("Hide", 0);
        }
        Hide = !Hide;
    }
}
