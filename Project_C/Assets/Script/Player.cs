using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Player : Object
{
    // Start is called before the first frame update
    [SerializeField]
    int Hp;
    int Mp;
    int Atk;
    int Def;
    [SerializeField]
    float Speed;

    STATE Curr_State;
    STATE Prev_State;

    [SerializeField]
    Monster Target;

    //스킬
    [SerializeField]
    Skill skill_shot;
    //인벤토리 - 아이템

    void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        Move();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("q"))
        {
            Debug.Log("Player_attack");
            DoAttack(Target);
        }
    }
    public override void UpdateData()
    {
        Debug.Log("Player_UpdateData");
        Init();
    }
    IEnumerator asd()
    {
        yield return null;
    }
    void Init()
    {
        Hp = 100;
        Mp = 100;
        Atk = 50;
        Def = 5;
        Speed = 5.5f;
        Curr_State = STATE.NONE;
        Prev_State = STATE.NONE;
    }
    void Move() 
    {
        float fx = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float fz = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        transform.Translate(fx, 0.0f, fz, Space.World);
    }
    void ChangeState(STATE newstate)
    {
        Prev_State = Curr_State;
        Curr_State = newstate;
    }
    void DoAttack(Monster monster)
    {
        monster.Hit(Atk);
    }
    public void Hit(int damage)
    {
        int value = Hp + (Def - damage);
        Hp = value;
    }
    void CheckHP()
    {
        if(Hp <= 0)
        {
            ChangeState(STATE.DIE);
        }
    }
    void GetItem()
    {
        // 가장 가까운 아이템 탐색 후 습득 - 인벤토리 적립
        
        // 필드 아이템 리스트 -> 검색 -> 습득
    }

    void Buff(int buff_stat,BUFF_TYPE type)
    {
        switch(type)
        {
            case BUFF_TYPE.HP:
                break;
            case BUFF_TYPE.MP:
                break;
            case BUFF_TYPE.ATK:
                break;
            case BUFF_TYPE.DEF:
                break;
            case BUFF_TYPE.SPEED:
                break;
        }
    }
    void DeBuff(int debuff_stat,BUFF_TYPE type)
    {
        switch (type)
        {
            case BUFF_TYPE.HP:
                break;
            case BUFF_TYPE.MP:
                break;
            case BUFF_TYPE.ATK:
                break;
            case BUFF_TYPE.DEF:
                break;
            case BUFF_TYPE.SPEED:
                break;
        }
    }
}
