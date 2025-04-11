using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    float Speed;
    float Atk;
    public void Init(Vector3 _pos,float _speed, float _atk,float _height)
    {
        transform.position = _pos;
        transform.position += Vector3.up * _height;
        Speed = _speed;
        Atk = _atk;
    }
    private void FixedUpdate()
    {
        transform.position += Vector3.down * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Shared.ParticleMgr.CreateParticle("StoneHit", transform, 0.3f);
        Destroy(gameObject);
    }
}
