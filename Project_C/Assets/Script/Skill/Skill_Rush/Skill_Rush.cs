using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Rush : Skill
{
    public float Speed;
    public float Distance;
    public Collider HITBOX;

    public override void UseSkill()
    {
        base.UseSkill();
        StartCoroutine(IRush());
    }

    IEnumerator IRush()
    {
        HITBOX.enabled = true;
        float dist = 0;
        Vector3 orgPos = Owner.transform.position;

        if(Owner.GetComponent<Character>().GetCharacterType() == 
            (int)CHARACTER_TYPE.PLAYER)
        {
            Player player = null;
            if (Owner.TryGetComponent<Player>(out player))
            {
                if(player.GetPVIsMine())
                {
                    Shared.MainCamera.ZoomEndStage(0.2f, 1.0f, 0.3f, 0.3f, 0.3f, Vector3.zero);
                }
            }
        }


        while (dist < Distance)
        {
            dist = Vector3.Distance(orgPos, Owner.transform.position);
            Owner.transform.Translate(Vector3.forward * Speed * Time.deltaTime);

            if (dist >= Distance)
            {
                HITBOX.enabled = false;
                base.SkillEnd();
                Owner.GetComponent<Character>().OnAniEnd();
                yield break;
            }
            yield return null;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Owner)
            return;

        bool IsPlayer = Shared.GameMgr.IsCheckCharacterType(other, (int)CHARACTER_TYPE.PLAYER);
        bool IsMonster = Shared.GameMgr.IsCheckCharacterType(other, (int)CHARACTER_TYPE.MONSTER);

        if (IsPlayer || IsMonster)
        {
            DamageData damgedata = Shared.GameMgr.DamageDataPool.Get(Atk, DAMAGEFONT_TYPE.YELLOW);
            other.gameObject.GetComponent<Character>().Hit(damgedata);
            
            // 충돌한 other와의 중간거리에 충돌이펙트 생성
            //GameObject hitpoint = Shared.GameMgr.GetMiddleObj(transform.position, other.transform.position);
            Character character = other.GetComponent<Character>();
            Shared.ParticleMgr.CreateParticle("BlueHit", character.GetBodyParticlePointObj().transform, 0.7f);
        }
    }
}
