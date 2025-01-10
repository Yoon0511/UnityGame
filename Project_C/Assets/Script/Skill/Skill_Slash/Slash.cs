using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    float Atk;
    float Speed = 5.0f;
    public void Init(Vector3 _pos,float _atk,float _speed)
    {
        Atk = _atk;
        Speed = _speed;
        transform.position = _pos;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TAG_MONSTER"))
        {
            other.GetComponent<Monster>().Hit(Atk);
            Destroy(gameObject);
        }
    }
}
