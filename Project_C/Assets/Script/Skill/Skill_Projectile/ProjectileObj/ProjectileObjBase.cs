using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileObjBase : MonoBehaviour
{
    public abstract void Init(float _atk,GameObject _target = null);
}
