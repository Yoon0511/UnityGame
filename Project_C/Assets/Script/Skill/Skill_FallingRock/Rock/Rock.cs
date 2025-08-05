using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    float Speed;
    float Atk;
    float StunDuration;
    Vector3 RotateDir;

    public void Init(Vector3 _pos,float _speed, float _atk,float _height,float _stunduration)
    {
        transform.position = _pos;
        transform.position += Vector3.up * _height;
        Speed = _speed;
        Atk = _atk;
        StunDuration = _stunduration;

        float rv = Random.Range(-50f, 50f);
        RotateDir = new Vector3(rv, rv, rv);
    }
    private void FixedUpdate()
    {
        transform.position += Vector3.down * Speed * Time.deltaTime;
        transform.Rotate(RotateDir * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Shared.ParticleMgr.CreateParticle("StoneHit", transform, 0.3f);
        
        if(Shared.GameMgr.IsCheckCharacterType(other,(int)CHARACTER_TYPE.PLAYER))
        {
            StunDuration = 1.0f;
            DeBuff Stun = new DeBuff_Stun(StunDuration, other.gameObject, "UI_Skill_Icon_Blackhole");
            other.gameObject.GetComponent<Character>().AddDeBuff(Stun);
        }
        Destroy(gameObject);
    }
}
