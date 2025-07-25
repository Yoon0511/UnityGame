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

    //��ȭ ����
    public void Init(NPC _npc,Player _player)
    {
        Player = _player;
        CurrentNPC = _npc;
        //�� �Ķ���
        NPC_NAME.text = "<color=#87CEFA><b>" + CurrentNPC.GetOnlyCharacterName() + "</b></color>";

        ConverstaionTexts = CurrentNPC.GetConverstationTexts();
        TextIndex = 0;
        MaxTextIndex = ConverstaionTexts.Length;
        EndConverstationIndex = MaxTextIndex - 1;
        //NPC_CONVERSTATIONTEXT.text = ConverstaionTexts[TextIndex];
        TypeCorutine = StartCoroutine(ITypingText(ConverstaionTexts[TextIndex]));

        ShowBtns();

        //�ó׸ӽ� ī�޶� - NPC�� �÷�� �߽����� ����
        Shared.CineMachineMgr.SetCinemachineTargetGroup(Player.transform, CurrentNPC.transform);
    }
    

    public void OnDialogueNextBtn()
    {
        TextIndex++;

        //���� ��ȭ��� ����
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

        //��ȭ����
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
        //����,������ư ǥ��
        if (TextIndex >= EndConverstationIndex)
        {
            //����Ʈ�� ������ �ִ� NPC
            QuestNPC QuestNPC = CurrentNPC as QuestNPC;
            if (QuestNPC == null) // �Ϲ� NPC�� ���
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

    //��ȭâ����
    void DialogueEnd()
    {
        //�ó׸ӽ� ī�޶� ����
        Shared.CineMachineMgr.EndCineMachine();

        DialogueReset();
        gameObject.SetActive(false);
    }
}
