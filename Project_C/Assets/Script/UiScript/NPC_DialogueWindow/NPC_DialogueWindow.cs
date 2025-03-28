using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_DialogueWindow : MonoBehaviour
{
    public Text NPC_NAME;
    public Text NPC_CONVERSTATIONTEXT;
    public Text BTN_TEXT;
    List<string> List_ConverstaionTexts = new List<string>();
    int TextIndex = 0;
    int MaxTextIndex = 0;

    public void Init(NPC _npc)
    {
        NPC_NAME.text = _npc.GetCharacterName();
        List_ConverstaionTexts = _npc.GetConverstationTexts();
        MaxTextIndex = List_ConverstaionTexts.Count;
    }
    
    public void InputConverstaionText(string text)
    {
        List_ConverstaionTexts.Add(text);
    }

    public void OnDialogueNextBtn()
    {
        //다음 대화대사 진행
        if(TextIndex < MaxTextIndex-1)
        {
            TextIndex++;
            Debug.Log(TextIndex + "---" + MaxTextIndex);
            NPC_CONVERSTATIONTEXT.text = List_ConverstaionTexts[TextIndex];

            if (TextIndex >= MaxTextIndex-1)
            {
                BTN_TEXT.text = "END";
            }
        }
    }

    public void OnDisable()
    {
        //대화창꺼짐
        List_ConverstaionTexts.Clear();
        BTN_TEXT.text = "NEXT";
        TextIndex = 0;
        MaxTextIndex = TextIndex;
    }
}
