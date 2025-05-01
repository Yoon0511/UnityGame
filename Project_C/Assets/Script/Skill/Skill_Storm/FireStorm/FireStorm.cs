using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStorm : MonoBehaviour
{
    Vector3 startPoint;
    Vector3 controlPoint;
    Vector3 endPoint;
    public float moveSpeed = 0.3f;

    private float progress = 0f;
    float DistX = 0f;
    float DistZ = 0f;
    float Atk = 0f;

    public void Init(float _distx,float _distz,float _atk)
    {
        Atk = _atk;

        DistX = _distx;
        DistZ = _distz;

        startPoint = transform.localPosition;

        endPoint = startPoint + transform.forward * _distz;
        endPoint.z += _distx;

        float ControlZ = DistZ * 0.8f;
        float ControlX = DistX * 0.3f;

        controlPoint.z = startPoint.z + ControlZ;
        controlPoint.x = startPoint.x + ControlX;
    }

    private void FixedUpdate()
    {
        progress += moveSpeed * Time.deltaTime;
        progress = Mathf.Clamp01(progress);
        
        //Vector3 m1 = Vector3.Slerp(startPoint, controlPoint, progress);
        //Vector3 m2 = Vector3.Slerp(controlPoint, endPoint, progress);
        //transform.localPosition = Vector3.Slerp(m1, m2, progress);
        transform.localPosition = Vector3.Slerp(startPoint, endPoint, progress);

        if (progress >= 1.0f)
        {
            Destroy(gameObject);
        }
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
