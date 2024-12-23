using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : MonoBehaviour
{
    [SerializeField]
    Monster monster;
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TAG_PLAYER"))
        {
            other.GetComponent<Player>().Hit(monster.GetAtk());
        }
    }
}
