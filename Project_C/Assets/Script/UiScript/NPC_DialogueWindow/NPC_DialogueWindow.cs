using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_DialogueWindow : MonoBehaviour
{
    public Text NPC_NAME;
    public Text NPC_CONVERSTATIONTEXT;
    public Text BTN_TEXT;
    public GameObject BTN_QUESTACCEPT;
    public GameObject BTN_QUESTREFUSAL;

    List<string> List_ConverstaionTexts = new List<string>();

    int TextIndex = 0;
    int MaxTextIndex = 0;
    int EndConverstationIndex = 0;
    NPC CurrentNPC;
    Player Player;
    public void Init(NPC _npc,Player _player)
    {
        Player = _player;
        CurrentNPC = _npc;
        NPC_NAME.text = CurrentNPC.GetCharacterName();
        List_ConverstaionTexts = CurrentNPC.GetConverstationTexts();
        TextIndex = 0;
        MaxTextIndex = List_ConverstaionTexts.Count;
        EndConverstationIndex = MaxTextIndex - 1;
        NPC_CONVERSTATIONTEXT.text = List_ConverstaionTexts[TextIndex];
    }
    
    public void InputConverstaionText(string text)
    {
        List_ConverstaionTexts.Add(text);
    }

    public void OnDialogueNextBtn()
    {
        TextIndex++;

        //다음 대화대사 진행
        if(TextIndex <= EndConverstationIndex)
            NPC_CONVERSTATIONTEXT.text = List_ConverstaionTexts[TextIndex];

        if(TextIndex >= EndConverstationIndex)
        {
            BTN_QUESTACCEPT.SetActive(true);
            BTN_QUESTREFUSAL.SetActive(true);
            BTN_TEXT.text = "END";
        }

        if(TextIndex >= MaxTextIndex)
        {
            DialogueEnd();
        }
    }

    public void OnQusetAccecpt()
    {
        CurrentNPC.QuestAccpect(Player);
        DialogueEnd();
    }

    public void OnQuestRefusal()
    {
        CurrentNPC.QuestRefusal();
        DialogueEnd();
    }

    public void DialogueReset()
    {
        BTN_QUESTACCEPT.SetActive(false);
        BTN_QUESTREFUSAL.SetActive(false);
        BTN_TEXT.text = "NEXT";
        TextIndex = 0;
        MaxTextIndex = 0;
        EndConverstationIndex = 0;
        CurrentNPC = null;
    }

    //대화창꺼짐
    void DialogueEnd()
    {
        DialogueReset();
        gameObject.SetActive(false);
    }
}
