using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    float Atk;
    float Speed = 5.0f;
    public void Init(Quaternion _rotate,Vector3 _pos,float _atk,float _speed)
    {
        Atk = _atk;
        Speed = _speed;
        transform.rotation = _rotate;
        transform.position = _pos;
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
            GameObject hitpoint = Shared.GameMgr.GetMiddleObj(transform.position, other.transform.position);
            Shared.ParticleMgr.CreateParticle("DarkHit", hitpoint.transform, 0.7f);

            other.GetComponent<Character>().Hit(Atk);
            Destroy(gameObject);
        }
    }
}
