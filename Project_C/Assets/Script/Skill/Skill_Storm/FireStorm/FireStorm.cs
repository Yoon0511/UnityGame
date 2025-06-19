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

        // ���濡�� _angleOffset��ŭ ȸ���� ���� ���
        Quaternion rotation = Quaternion.Euler(0, _angleOffset, 0);
        MoveDir = rotation * _forwardDir;

        // �̵� ���� �������� �ð������� ȸ��
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
            // ������ ó��
            DamageData damageData = Shared.GameMgr.DamageDataPool.Get(Atk,DAMAGEFONT_TYPE.YELLOW);
            otherCharacter.Hit(damageData);

            // ��ƼŬ ���
            GameObject BodyParticle = otherCharacter.GetComponent<Character>().GetBodyParticlePointObj();
            Shared.ParticleMgr.CreateParticle("RedHit", BodyParticle.transform, 0.5f);
            //����
            Destroy(gameObject);
        }
    }
}
