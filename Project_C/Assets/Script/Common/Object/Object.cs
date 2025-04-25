using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    float Duration;
    float Veclocity;

    Vector3 Orgpos = Vector3.zero;
    public void Shake(float _duration, float _veclocity)
    {
        Duration = _duration;
        Veclocity = _veclocity;

        Orgpos = transform.localPosition;

        StartCoroutine(IShake());
    }

    IEnumerator IShake()
    {
        float elapsed = 0.0f;

        while (elapsed < Duration)
        {
            float rx = Random.Range(-1f, 1f) * Veclocity;
            float ry = Random.Range(-1f, 1f) * Veclocity;

            transform.localPosition = Orgpos + new Vector3(rx, ry, 0.0f);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = Orgpos;
    }
}
