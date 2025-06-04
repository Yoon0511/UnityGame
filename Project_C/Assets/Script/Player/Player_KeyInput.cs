using System.Collections;
using System.Collections.Generic;
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
            HP_Postion obj = (HP_Postion)Shared.DataMgr.GetItem(1016);

            Inventory.AddItem(obj);
        }

        if (Input.GetKeyDown(KeyCode.F5)) //무기
        {
            // 1 26 42
            // 2 29 45
            // Weapon obj = Instantiate(weapon).GetComponent<Weapon>();
            Weapon obj = (Weapon)Shared.DataMgr.GetItem(1001);

            Inventory.AddItem(obj);
            //equiment.EquippedItem(weapon);
        }
        if (Input.GetKeyDown(KeyCode.F6)) //갑옷
        {
            //Amor obj = Instantiate(amor).GetComponent<Amor>();
            Amor obj = (Amor)Shared.DataMgr.GetItem(1006);

            Inventory.AddItem(obj);
        }
        if (Input.GetKeyDown(KeyCode.F7)) //악세서리
        {
            //Accessories obj = Instantiate(ring).GetComponent<Accessories>();
            Accessories obj = (Accessories)Shared.DataMgr.GetItem(1011);

            Inventory.AddItem(obj);
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

        KeyBoardMove();
    }

    void KeyBoardMove()
    {
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
            JoystickMove(dir.normalized, (1 / IsWalk) * dist, true);
        }

        if (MoveKeyDown)
        {
            JoystickMove(dir.normalized, (1 / IsWalk) * dist, true);
        }
    }
}
