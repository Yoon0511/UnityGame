using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.Cockpit;
using UnityEngine;

public partial class Monster
{
    [SerializeField]
    protected Transform[] PatrolPointTransform;
    protected Vector3[] patrolPoint;
    protected int patrolIndex = 0;
    public override void Fsm_Init()
    {
        Fsm = new StateMachine(new Monster_IdleState(this));
        CurrState = (int)MONSTER_STATE.IDLE;
        PrevState = CurrState;

        DicState.Add((int)MONSTER_STATE.IDLE, new Monster_IdleState(this));
        DicState.Add((int)MONSTER_STATE.MOVE, new Monster_MoveState(this));
        DicState.Add((int)MONSTER_STATE.PATROL, new Monster_PatrolState(this));
        DicState.Add((int)MONSTER_STATE.CHASE, new Monster_ChaseState(this));
        DicState.Add((int)MONSTER_STATE.ATTACK, new Monster_AttackState(this));
        DicState.Add((int)MONSTER_STATE.DIE, new Monster_DieState(this));

        Fsm.ChangeState(DicState[(int)MONSTER_STATE.IDLE]);

        PatrolPointInit();
    }

    public bool IsPlayerInDetectionRange()
    {
        for(int i = 0; i < ListPlayer.Count; ++i)
        {
            float dist = DistXZ(ListPlayer[i].transform.position, transform.position);
            if (dist <= detectionRange)
            {
                ChangeTarget(ListPlayer[i].gameObject);
                return true;
            }
        }

        //float dist = DistXZ(ListPlayer[i].transform.position, transform.position);
        //if (dist <= detectionRange)
        //{
        //    return true;
        //}

        return false;
    }

    /////////////////// 범위 테스트 ///////////////////
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    //float dist = Vector3.Distance(transform.position, Target.transform.position);
    //    //Debug.Log(dist);
    //    Gizmos.DrawSphere(transform.position, detectionRange);
    //
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, attackRange);
    //}
    /////////////////// 범위 테스트 ///////////////////
    public bool IsPlayerInAttackRange()
    {
        //float dist = DistXZ(Target.transform.position, transform.position);
        //if (dist <= attackRange)
        //{
        //    return true;
        //}

        for (int i = 0; i < ListPlayer.Count; ++i)
        {
            float dist = DistXZ(ListPlayer[i].transform.position, transform.position);
            if (dist <= attackRange)
            {
                return true;
            }
        }
        return false;
    }

    public void PatrolModeInit()
    {
        patrolIndex = 0;
    }
    public void PatrolMode()
    {
        float speed = Statdata.GetData(STAT_TYPE.SPEED);
        Vector3 dir = patrolPoint[patrolIndex] - transform.position;
        dir.y = 0f; //y축 거리는 제외

        Vector3 MovePos = dir.normalized * speed * Time.deltaTime;
        transform.Translate(MovePos, Space.World);

        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

        //y축을 제외한 거리 계산
        float dist = DistXZ(transform.position, patrolPoint[patrolIndex]);

        if (dist <= 0.5f)
        {
            patrolIndex++;
            if (patrolIndex >= patrolPoint.Length)
            {
                patrolIndex = 0;
            }
        }
    }

    public float DistXZ(Vector3 _a,Vector3 _b)
    {
        Vector2 a = new Vector2(_a.x, _a.z);
        Vector2 b = new Vector2(_b.x, _b.z);
        float dist = Vector2.Distance(a, b);
        return dist;
    }

    public void SetPatrolIndex(int _index)
    {
        patrolIndex = _index;
    }

    public void StartChageToPatrol(float _time)
    {
        StartCoroutine(IChageToPatrol(_time));
    }
    private IEnumerator IChageToPatrol(float _time)
    {
        yield return new WaitForSeconds(_time);
        ChangeState((int)MONSTER_STATE.PATROL);
    }

    protected void PatrolPointInit()
    {
        patrolPoint = new Vector3[PatrolPointTransform.Length];
        for (int i = 0; i < PatrolPointTransform.Length; i++)
        {
            patrolPoint[i] = PatrolPointTransform[i].position;
        }
    }
}
