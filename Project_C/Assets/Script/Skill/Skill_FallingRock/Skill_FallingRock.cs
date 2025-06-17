using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Skill_FallingRock : Skill
{
    public GameObject AtkRangeCircle;
    public GameObject[] Rock;
    public int ROCKCOUNT;
    public float ROCK_MIN_SPEED = 3;
    public float ROCK_MAX_SPEED = 10;
    public float MIN_STUN_DURATION = 0.2f;
    public float MAX_STUN_DURATION = 0.5f;

    List<AtkRange> ListAtkRangeCircle = new List<AtkRange>();

    string RockPrefapPath = "Prefabs/Skill/SKill_FallingRock/Rock/Rock";

    public override void UseSkill()
    {
        base.UseSkill();
        Player player = Owner.GetComponent<Character>() as Player;
        if(player != null)
        {
            if(player.GetPVIsMine() == false)
            {
                return;
            }
        }

        if(Owner.GetComponent<Character>().GetCharacterType() ==
            (int)CHARACTER_TYPE.MONSTER)
        {
            //몬스터가 사용시 카메라 흔들기
            //Shared.MainCamera.Shake(0);
            Vector3[] RpcAtkPos = new Vector3[ROCKCOUNT];
            float[] RpcAtkTime = new float[ROCKCOUNT];

            //공격 범위 생성
            for (int i = 0; i < ROCKCOUNT; i++)
            {
                float RandomX = Random.Range(-15f, 15f);
                float RandomZ = Random.Range(-15f, 15f);
                Vector3 randpos = new Vector3(RandomX, 0, RandomZ);
                Vector3 pos = Owner.transform.position + randpos;
                pos.y = Shared.GameMgr.GetTerrainHeight(pos) + 0.1f;

                //GameObject AtkCircle = Instantiate(AtkRangeCircle, pos, Quaternion.identity);
                GameObject AtkCircle = Shared.PoolMgr.GetObject("AtkRange_Circle");
                AtkCircle.transform.position = pos;
                AtkCircle.GetComponent<AtkRange>().Init(false);
                AtkCircle.GetComponent<AtkRange>().SetDesiredTime(Random.Range(3.0f, 10.0f));
                ListAtkRangeCircle.Add(AtkCircle.GetComponent<AtkRange>());

                //Rpc
                RpcAtkPos[i] = AtkCircle.transform.position;
                RpcAtkTime[i] = AtkCircle.GetComponent<AtkRange>().GetDesiredTime();
            }
            Shared.PhotonMgr.SendAtkRange(RpcAtkPos, RpcAtkTime);

            StartCoroutine(IFallingRock());
        }

        Player Player;
        if(Owner.transform.TryGetComponent(out Player))
        {
            Vector3[] RpcAtkPos = new Vector3[ROCKCOUNT];
            float[] RpcAtkTime = new float[ROCKCOUNT];

            //공격 범위 생성
            for (int i = 0; i < ROCKCOUNT; i++)
            {
                float RandomX = Random.Range(-15f, 15f);
                float RandomZ = Random.Range(-15f, 15f);
                Vector3 randpos = new Vector3(RandomX, 0, RandomZ);
                Vector3 pos = Owner.transform.position + randpos;
                pos.y = Shared.GameMgr.GetTerrainHeight(pos) + 0.1f;

                //GameObject AtkCircle = Instantiate(AtkRangeCircle, pos, Quaternion.identity);
                GameObject AtkCircle = Shared.PoolMgr.GetObject("AtkRange_Circle");
                AtkCircle.transform.position = pos;
                AtkCircle.GetComponent<AtkRange>().Init(false);
                AtkCircle.GetComponent<AtkRange>().SetDesiredTime(Random.Range(3.0f, 10.0f));
                ListAtkRangeCircle.Add(AtkCircle.GetComponent<AtkRange>());

                //Rpc
                RpcAtkPos[i] = AtkCircle.transform.position;
                RpcAtkTime[i] = AtkCircle.GetComponent<AtkRange>().GetDesiredTime();
            }
            Shared.PhotonMgr.SendAtkRange(RpcAtkPos, RpcAtkTime);

            StartCoroutine(IFallingRock());
        }
        
        base.SkillEnd();
    }

    //private void FixedUpdate()
    //{
    //    int i = 0;
    //    if(ListAtkRangeCircle.Count > 0)
    //    {
    //        i = i % ListAtkRangeCircle.Count;
    //        if (ListAtkRangeCircle[i].IsStretchEnd())
    //        {
    //            //돌 생성
    //            int random = Random.Range(0, Rock.Length);
    //            //GameObject rockobj = Instantiate(Rock[random]);
    //            GameObject rockobj = PhotonNetwork.Instantiate(RockPrefapPath, ListAtkRangeCircle[i].transform.position,Quaternion.identity,0);
    //            float RockSpeed = Random.Range(ROCK_MIN_SPEED, ROCK_MAX_SPEED);
    //            rockobj.GetComponent<Rock>().Init(ListAtkRangeCircle[i].transform.position,
    //                RockSpeed, Atk, Random.Range(4.0f, 7.0f), Random.Range(MIN_STUN_DURATION, MAX_STUN_DURATION));
    //
    //            Destroy(ListAtkRangeCircle[i].gameObject);
    //            ListAtkRangeCircle.RemoveAt(i);
    //        }
    //        //else
    //        //{
    //        //    ListAtkRangeCircle[i].StartSizeUp();
    //        //}
    //        ++i;
    //    }
    //}

    IEnumerator IFallingRock()
    {
        int i = 0;
        while(ListAtkRangeCircle.Count > 0)
        {
            i = i % ListAtkRangeCircle.Count;
            if (ListAtkRangeCircle[i].IsStretchEnd())
            {
                //돌 생성
                int random = Random.Range(0, Rock.Length);
                //GameObject rockobj = Instantiate(Rock[random]);
                GameObject rockobj = PhotonNetwork.Instantiate(RockPrefapPath, ListAtkRangeCircle[i].transform.position,Quaternion.identity,0);
                float RockSpeed = Random.Range(ROCK_MIN_SPEED, ROCK_MAX_SPEED);
                rockobj.GetComponent<Rock>().Init(ListAtkRangeCircle[i].transform.position, 
                    RockSpeed, Atk,Random.Range(4.0f,7.0f), Random.Range(MIN_STUN_DURATION, MAX_STUN_DURATION));
                
                Destroy(ListAtkRangeCircle[i].gameObject);
                ListAtkRangeCircle.RemoveAt(i);
            }
            //else
            //{
            //    ListAtkRangeCircle[i].StartSizeUp();
            //}
            ++i;
            yield return null;
        }
        if(ListAtkRangeCircle.Count == 0)
        {
            StopCoroutine(IFallingRock());
        }
    }
}
