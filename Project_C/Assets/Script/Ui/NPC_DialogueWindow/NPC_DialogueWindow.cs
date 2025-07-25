using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_DialogueWindow : MonoBehaviour
{
    public Text NPC_NAME;
    public Text NPC_CONVERSTATIONTEXT;

    public GameObject BTN_QUESTACCEPT;
    public GameObject BTN_QUESTREFUSAL;
    public GameObject BTN_NEXT;
    public GameObject BTN_COMPLETE;

    string[] ConverstaionTexts;

    int TextIndex = 0;
    int MaxTextIndex = 0;
    int EndConverstationIndex = 0;
    NPC CurrentNPC;
    Player Player;

    bool Typing = false;
    Coroutine TypeCorutine;

    //대화 시작
    public void Init(NPC _npc,Player _player)
    {
        Player = _player;
        CurrentNPC = _npc;
        //빛 파란색
        NPC_NAME.text = "<color=#87CEFA><b>" + CurrentNPC.GetOnlyCharacterName() + "</b></color>";

        ConverstaionTexts = CurrentNPC.GetConverstationTexts();
        TextIndex = 0;
        MaxTextIndex = ConverstaionTexts.Length;
        EndConverstationIndex = MaxTextIndex - 1;
        //NPC_CONVERSTATIONTEXT.text = ConverstaionTexts[TextIndex];
        TypeCorutine = StartCoroutine(ITypingText(ConverstaionTexts[TextIndex]));

        ShowBtns();

        //시네머신 카메라 - NPC와 플레어를 중심으로 설정
        Shared.CineMachineMgr.SetCinemachineTargetGroup(Player.transform, CurrentNPC.transform);
    }
    

    public void OnDialogueNextBtn()
    {
        TextIndex++;

        //다음 대화대사 진행
        if(TextIndex <= EndConverstationIndex)
        {
            if(TypeCorutine != null)
            {
                if(Typing)
                {
                    StopCoroutine(TypeCorutine);
                    NPC_CONVERSTATIONTEXT.text = ConverstaionTexts[TextIndex];
                }
                else
                {
                    TypeCorutine = StartCoroutine(ITypingText(ConverstaionTexts[TextIndex]));
                }
            }
            //NPC_CONVERSTATIONTEXT.text = ConverstaionTexts[TextIndex];
        }

        ShowBtns();

        //대화종료
        if (TextIndex >= MaxTextIndex)
        {
            DialogueEnd();
        }
    }

    IEnumerator ITypingText(string _text)
    {
        NPC_CONVERSTATIONTEXT.text = "";
        
        for (int i = 0;i<_text.Length;i++)
        {
            Typing = true;
            NPC_CONVERSTATIONTEXT.text += _text[i];
            yield return new WaitForSeconds(0.05f);
        }
        Typing = false;
        yield return null;
    }

    public void ShowBtns()
    {
        //수락,거절버튼 표시
        if (TextIndex >= EndConverstationIndex)
        {
            //퀘스트를 가지고 있는 NPC
            QuestNPC QuestNPC = CurrentNPC as QuestNPC;
            if (QuestNPC == null) // 일반 NPC일 경우
                return;

            switch(QuestNPC.GetQuest().GetQuestState())
            {
                case QUEST_STATE.START:
                    BTN_QUESTACCEPT.SetActive(true);
                    BTN_QUESTREFUSAL.SetActive(true);
                    BTN_NEXT.SetActive(false);
                    break;
                case QUEST_STATE.PROGRESS:
                    break;
                case QUEST_STATE.COMPLETE:
                    BTN_QUESTACCEPT.SetActive(false);
                    BTN_QUESTREFUSAL.SetActive(false);
                    BTN_NEXT.SetActive(false);
                    BTN_COMPLETE.SetActive(true);
                    break;
            }
        }
    }

    public void OnQusetAccecpt()
    {
        QuestNPC QuestNPC = CurrentNPC as QuestNPC;
        if(QuestNPC != null)
        {
            QuestNPC.QuestAccpect(Player);
        }
        DialogueEnd();
    }

    public void OnQuestRefusal()
    {
        QuestNPC QuestNPC = CurrentNPC as QuestNPC;
        if (QuestNPC != null)
        {
            QuestNPC.QuestRefusal();
        }
        DialogueEnd();
    }

    public void OnQuestComplete()
    {
        QuestNPC QuestNPC = CurrentNPC as QuestNPC;
        if (QuestNPC != null)
        {
            QuestNPC.QuestComplete();
        }
        DialogueEnd();
    }
    public void DialogueReset()
    {
        BTN_QUESTACCEPT.SetActive(false);
        BTN_QUESTREFUSAL.SetActive(false);
        BTN_COMPLETE.SetActive(false);
        BTN_NEXT.SetActive(true);
        TextIndex = 0;
        MaxTextIndex = 0;
        EndConverstationIndex = 0;
        CurrentNPC = null;
    }

    //대화창꺼짐
    void DialogueEnd()
    {
        //시네머신 카메라 종료
        Shared.CineMachineMgr.EndCineMachine();

        DialogueReset();
        gameObject.SetActive(false);
    }
}
