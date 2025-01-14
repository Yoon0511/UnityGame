using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBase : MonoBehaviour
{
    protected GameObject Character;
    protected int TargetIndex = 0;
    protected eAI AISate = eAI.eAI_NONE;

    public void Init(GameObject character)
    {
        Character = character;
        TargetIndex = 0;
    }

    public void State()
    {
        switch(AISate)
        {
           case eAI.eAI_CREATE:
                break;
           case eAI.eAI_SEARCH: 
                break;
           case eAI.eAI_MOVE:
                break;
           case eAI.eAI_RESET:
                break;
        }
    }

    protected virtual void Create()
    {
        AISate = eAI.eAI_CREATE;
    }
    protected virtual void Search()
    {
        AISate = eAI.eAI_SEARCH;
    }
    protected virtual void Move()
    {
        AISate = eAI.eAI_MOVE;
    }
    protected virtual void  Reset()
    {
        AISate = eAI.eAI_SEARCH;
    }

}
