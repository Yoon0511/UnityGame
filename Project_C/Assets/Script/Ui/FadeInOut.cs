using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField]
    Image FadeImage;

    [SerializeField]
    float FadeDuration = 10.0f;

    public void SetFadeDuration(float _duration)
    {
        FadeDuration = _duration;
    }
    public void FadeIn()
    {
        StartCoroutine(Fade(1, 0));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0, 1));
    }

    private IEnumerator Fade(float from, float to)
    {
        float time = 0f;
        Color color = FadeImage.color;

        while (time < FadeDuration)
        {
            float t = time / FadeDuration;
            color.a = Mathf.Lerp(from, to, t);
            FadeImage.color = color;
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        color.a = to;
        FadeImage.color = color;
    }
}
