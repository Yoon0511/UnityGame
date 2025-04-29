using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileObjBase : MonoBehaviour
{
    public abstract void Init(GameObject _target = null);
}
