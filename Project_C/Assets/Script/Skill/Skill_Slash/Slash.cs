using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    float Atk;
    float Speed = 5.0f;
    float DeleteTime = 3.0f;
    public void Init(Quaternion _rotate,Vector3 _pos,float _atk,float _speed)
    {
        Atk = _atk;
        Speed = _speed;
        transform.rotation = _rotate;
        transform.position = _pos;
        StartCoroutine(ISlash());
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
    public void OnTriggerEnter(Collider other)
    {
        bool check = Shared.GameMgr.IsCheckCharacterType(other, (int)CHARACTER_TYPE.MONSTER);
        if (check)
        {
            Character character = other.GetComponent<Character>();
            //GameObject hitpoint = Shared.GameMgr.GetMiddleObj(transform.position, other.transform.position);
            Shared.ParticleMgr.CreateParticle("DarkHit", character.GetBodyParticlePointObj().transform, 0.7f);

            DamageData damgedata = Shared.GameMgr.DamageDataPool.Get(Atk, DAMAGEFONT_TYPE.GREEN);
            other.GetComponent<Character>().Hit(damgedata);
            Destroy(gameObject);
        }
    }

    IEnumerator ISlash()
    {
        yield return new WaitForSeconds(DeleteTime);
        Destroy(gameObject);
    }
}
