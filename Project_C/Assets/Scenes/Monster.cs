using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class Monster : Object
{
    [SerializeField]
    int Hp;
    int Mp;
    int Atk;
    int Def;
    float Speed;

    STATE Curr_State;
    STATE Prev_State;

    [SerializeField]
    Player Target;

    [SerializeField]
    GameObject Item;
    //아이템드랍
    void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("e"))
        //{
        //    Debug.Log("Monster_attack");
        //    DoAttack(Target);
        //}
    }
    public override void UpdateData()
    {
        Debug.Log("Monster_UpdateData");
        Init();
    }

    void Init()
    {
        Hp = 100;
        Mp = 100;
        Atk = 10;
        Def = 5;
        Speed = 5.5f;
        Curr_State = STATE.NONE;
        Prev_State = STATE.NONE;
    }

    void DoAttack(Player player)
    {
        player.Hit(Atk);
    }
    void Move() { }
    void ChangeState(STATE newstate)
    {
        Prev_State = Curr_State;
        Curr_State = newstate;
    }
    public void Hit(int damage)
    {
        int value = Hp + (Def - damage);
        Hp = value;
        
        if(CheckHP())
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    bool CheckHP() 
    {
        if(Hp <= 0)
        {
            return true;
        }
        return false;
    }
    void DropItem()
    {
        Instantiate(Item,transform.position,transform.rotation);
    }
}
