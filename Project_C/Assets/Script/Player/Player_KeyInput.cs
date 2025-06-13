using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Player
{
    void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 공격
        {
            //ChangeState((int)STATE.ATTACK,(int)PLAYER_ANI_STATE.ATTACK);
            OnAttack();
        }
        if (IsComboEnable)
        {
            OnAttack();
        }

        //라이딩 테스트
        if (Input.GetKeyDown(KeyCode.F1))
        {
            OnRiding();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            OffRiding();
        }

        //가드 테스트
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Guard();
        }

        //테스트
        if (Input.GetKeyDown(KeyCode.F3)) //포션
        {
            //ItemBase obj = Instantiate(item).GetComponent<ItemBase>();
            for(int i = 1016;i<=1025;++i)
            {
                Potion obj = (Potion)Shared.DataMgr.GetItem(i);
               Inventory.AddItem(obj);
            }
        }

        if (Input.GetKeyDown(KeyCode.F5)) //무기
        {
            for(int i = 1001;i<=1005;++i)
            {
                Weapon obj = (Weapon)Shared.DataMgr.GetItem(i);
                Inventory.AddItem(obj);
            }

            //equiment.EquippedItem(weapon);
        }
        if (Input.GetKeyDown(KeyCode.F6)) //갑옷
        {
            for(int i = 1006;i<=1010;++i)
            {
                Amor obj = (Amor)Shared.DataMgr.GetItem(i);
                Inventory.AddItem(obj);
            }

        }
        if (Input.GetKeyDown(KeyCode.F7)) //악세서리
        {
            for (int i = 1011; i <= 1015; ++i)
            {
                Accessories obj = (Accessories)Shared.DataMgr.GetItem(i);
                Inventory.AddItem(obj);
            }
        }

        if (Input.GetKeyDown(KeyCode.F8)) //HP
        {
            Statdata.EnhanceStat(STAT_TYPE.HP, -100f);
            UpdateUnitFrame();
        }

        if (Input.GetKeyDown(KeyCode.F9)) //MP
        {
            //Statdata.EnhanceStat(STAT_TYPE.HP, 100f);
            //Statdata.EnhanceStat(STAT_TYPE.MP, -10f);
            AddExp(10f);
            //UpdateUnitFrame();
        }

        if (Input.GetMouseButtonDown(0))
        {
            ClickToRay();
        }

        if (Input.GetKeyDown(KeyCode.F10)) //퀘스트 테스트
        {
            //Debug.Log("hunting");
            HuntingMsg huntingMsg = new HuntingMsg();
            huntingMsg.SetMsg(10, 10, (int)QUEST_TYPE.HUNTING, (int)MONSTER_ID.GOLEM, 3);
            QusetProgress(huntingMsg);

            //DeBuff deBuff = new DeBuff_Stun(0.5f, gameObject, "UI_Skill_Icon_Blackhole");

            //도트데미지 테스트
            //DotDamage dot = new DotDamage(0.5f,STAT_TYPE.HP,30f,5f,gameObject, "UI_Skill_Icon_PsycicAttack");

            //AddBuff(dot);


            //골드 테스트
            AddGold(3000);
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            foreach (var player in Party)
            {
                if (player != null)
                {
                    Debug.Log(player.GetCharacterName());
                }
            }
        }

        //KeyBoardMove();
    }

    void KeyBoardMove()
    {
        if(Shared.GameMgr.JOYSTICK.IsDrag)
        {
            return;
        }

        bool MoveKeyDown = false;
        float IsWalk = 1;
        float w = 0.0f;
        float a = 0.0f;
        float s = 0.0f;
        float d = 0.0f;
        //float Speed = Statdata.GetData(STAT_TYPE.SPEED) / IsWalk;
        if (Input.GetKey(KeyCode.W))
        {
            w = 1;
            MoveKeyDown = true;
        }
        if(Input.GetKey(KeyCode.A))
        {
            a = -1;
            MoveKeyDown = true;
        }
        if(Input.GetKey(KeyCode.S))
        {
            s = -1;
            MoveKeyDown = true;
        }
        if(Input.GetKey(KeyCode.D))
        {
            d = 1;
            MoveKeyDown = true;
        }
        Vector3 dir = new Vector3(a + d, w + s, 0.0f);

        if(Input.GetKey(KeyCode.LeftShift))
        {
            IsWalk = 3;
            MoveKeyDown = true;
        }

        float dist = 1.0f;
        if(MoveKeyDown == false)
        {
            dist = 0;
            JoystickMove(dir.normalized, 0.0f, true);
        }

        if (MoveKeyDown)
        {
            JoystickMove(dir.normalized, (1 / IsWalk) * dist, true);
        }
    }
}
