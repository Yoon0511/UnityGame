using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDamageText : MonoBehaviour
{
    public GameObject DAMAGETEXT;

    public void Create(Transform _position, Color _color, float _damage)
    {
        GameObject _text = Instantiate(DAMAGETEXT, _position.position, Quaternion.identity);
        _text.GetComponent<DamageText>().Init(_position, _color, _damage);
    }
}
