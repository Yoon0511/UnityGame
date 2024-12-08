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
    float WalkSpeed;
    float RunSpeed;

    [SerializeField]
    STATE Curr_State;
    [SerializeField]
    STATE Prev_State;

    [SerializeField]
    Monster target;

    Animator animator;
    Rigidbody rb;
    
    //인벤토리 - 아이템

    void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        //키보드
        //Move();
    }
    // Update is called once per frame
    void Update()
    {
        //UpdateAnimation();
    }
    public override void UpdateData()
    {
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
        Speed = 8.0f;
        WalkSpeed = 5.5f;
        RunSpeed = 10.0f;
        Curr_State = STATE.IDLE;
        Prev_State = STATE.NONE;

        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    //키보드 조작
    void Move() 
    {
        if (Curr_State == STATE.ATTACK)
        {
            return;
        }

        float fx = Input.GetAxis("Horizontal");
        float fz = Input.GetAxis("Vertical");

        transform.Translate(fx * Speed * Time.deltaTime, 0.0f, fz *Speed * Time.deltaTime, Space.World);

        Vector3 dir = new Vector3(fx, 0.0f, fz).normalized;
        transform.LookAt(transform.position + dir, Vector3.up);

        if(fx == 0.0f && fz == 0.0f)
        {
            animator.SetInteger("Ani_State", (int)STATE.IDLE);
        }
        else
        {
            animator.SetInteger("Ani_State", (int)STATE.WALK);
        }
    }

    public void JoystickMove(Vector3 _dir,float _dist)
    {
        if (Curr_State == STATE.ATTACK)
        {
            return;
        }

        float DistSpeed = Speed * _dist;
        float fx = _dir.x * DistSpeed * Time.deltaTime;
        float fz = _dir.y * DistSpeed * Time.deltaTime;

        transform.Translate(fx, 0.0f, fz, Space.World);

        Vector3 dir = new Vector3(fx, 0.0f, fz).normalized;
        transform.LookAt(transform.position + dir, Vector3.up);

        animator.SetFloat("Ani_Speed", DistSpeed);

        if (DistSpeed >= Speed * 0.6f)
        {
            ChangeState(STATE.RUN);
        }
        else if(DistSpeed > Speed * 0.4f)
        {
            ChangeState(STATE.WALK);
        }
        else
        {
            ChangeState(STATE.IDLE);
        }
        /*
        if (fx == 0.0f && fz == 0.0f)
        { 
            animator.SetInteger("Ani_State", (int)STATE.IDLE);
            ChangeState(STATE.IDLE);
        }
        else
        {
            animator.SetInteger("Ani_State", (int)STATE.WALK);
            ChangeState(STATE.WALK);
        }*/
    }

    //키보드 조작
    void UpdateAnimation()
    {
        if (Curr_State == STATE.ATTACK)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) // 공격
        {
            animator.SetTrigger("Ani_ATK");
            ChangeState(STATE.ATTACK);
        }
        else if (Input.GetKey(KeyCode.LeftShift)) // 달리기
        {
            Speed = RunSpeed;
            animator.SetInteger("Ani_State", (int)STATE.RUN);
            ChangeState(STATE.RUN);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) // 걷기
        {
            Speed = WalkSpeed;
            animator.SetInteger("Ani_State", (int)STATE.WALK);
            ChangeState(STATE.WALK);
        }
    }

    public void OnAttack()
    {
        if (Curr_State == STATE.ATTACK)
        {
            return;
        }

        animator.SetTrigger("Ani_ATK");
        ChangeState(STATE.ATTACK);
    }

    void ChangeState(STATE _newstate)
    {
        if (Curr_State == _newstate)
           return;

        Prev_State = Curr_State;
        Curr_State = _newstate;
    }

    public void AniATKEnd()
    {
        ChangeState(Prev_State);
    }

    public STATE GetCurrState()
    {
        return Curr_State;
    }

    public STATE GetPrevState()
    {
        return Prev_State;
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

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TAG_MONSTER"))
        {
            other.GetComponent<Monster>().Hit(Atk);
        }
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