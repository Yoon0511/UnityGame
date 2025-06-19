using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireStorm : MonoBehaviour
{
    public float MoveSpeed = 0.3f;
    float Atk = 0f;
    Vector3 MoveDir;

    public void Init(float _atk,float _angleOffset, Vector3 _forwardDir)
    {
        Atk = _atk;

        // 전방에서 _angleOffset만큼 회전된 방향 계산
        Quaternion rotation = Quaternion.Euler(0, _angleOffset, 0);
        MoveDir = rotation * _forwardDir;

        // 이동 방향 기준으로 시각적으로 회전
        transform.rotation = Quaternion.LookRotation(MoveDir);
    }
    IEnumerator IAutoRemove()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        transform.position += MoveDir * MoveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool check = Shared.GameMgr.IsCheckCharacterType(other, (int)CHARACTER_TYPE.PLAYER);
        if(check)
        {
            Character otherCharacter = other.GetComponent<Character>();
            // 데미지 처리
            DamageData damageData = Shared.GameMgr.DamageDataPool.Get(Atk,DAMAGEFONT_TYPE.YELLOW);
            otherCharacter.Hit(damageData);

            // 파티클 출력
            GameObject BodyParticle = otherCharacter.GetComponent<Character>().GetBodyParticlePointObj();
            Shared.ParticleMgr.CreateParticle("RedHit", BodyParticle.transform, 0.5f);
            //삭제
            Destroy(gameObject);
        }
    }
}
