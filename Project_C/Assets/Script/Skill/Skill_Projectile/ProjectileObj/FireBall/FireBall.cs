using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : ProjectileObjBase
{
    Vector3 Dir = Vector3.zero;
    public float MoveSpeed = 14.0f;
    float Atk = 0f;

    public override void Init(float _atk, GameObject _target = null)
    {
        if (_target == null)
            return;
        Atk = _atk;
        Dir = (_target.transform.position - transform.position).normalized;
        Shared.ParticleMgr.CreateParticle("Explosion",transform,0.7f);
        Shared.SoundMgr.PlaySFX("FIRE_BALL");
    }

    private void FixedUpdate()
    {
        transform.Translate(Dir * MoveSpeed * Time.deltaTime,Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool check = Shared.GameMgr.IsCheckCharacterType(other, (int)CHARACTER_TYPE.PLAYER);
        
        if(check)
        {
            Character character = other.GetComponent<Character>();
            if(character != null)
            {
                DamageData damageData = Shared.GameMgr.DamageDataPool.Get(Atk, DAMAGEFONT_TYPE.YELLOW);
                character.Hit(damageData);
            }
        }
        Shared.ParticleMgr.CreateParticle("RedHit", transform, 0.5f);
        Destroy(gameObject);

    }
}
