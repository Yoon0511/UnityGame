using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : ProjectileObjBase
{
    Vector3 Dir = Vector3.zero;
    public float MoveSpeed = 14.0f;

    public override void Init(GameObject _target = null)
    {
        if (_target == null)
            return;

        Dir = (_target.transform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        transform.Translate(Dir * MoveSpeed * Time.deltaTime,Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Shared.ParticleMgr.CreateParticle("RedHit", transform, 0.5f);
        Destroy(gameObject);
    }
}
