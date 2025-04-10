using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : PoolAble
{
    public float Duration;

    private void OnEnable()
    {
        StartCoroutine(IReleaseObject());
    }

    IEnumerator IReleaseObject()
    {
        yield return new WaitForSeconds(Duration);

        ReleaseObject();
    }
}
