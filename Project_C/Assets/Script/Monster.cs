using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class Monster : Character
{
    [SerializeField]
    Player Target;

    [SerializeField]
    GameObject Item;

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
    public override void Init()
    {
        MaxHp = 100.0f;
        MaxMp = 100.0f;
        Hp = MaxHp;
        Mp = MaxMp;
        Atk = 10.0f;
        Def = 2.0f;
        Speed = 5.5f;
        Curr_State = STATE.NONE;
        Prev_State = STATE.NONE;
    }

    void DoAttack(Player player)
    {
        player.Hit(Atk);
    }
    void Move() { }
    public override void Hit(float damage)
    {
        float value = Hp + (Def - damage);
        Hp = value;

        Debug.Log("MONSTER HP - " + Hp);

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
