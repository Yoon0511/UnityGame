using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitFrame : MonoBehaviour
{
    public Image HP_BAR;
    public Image MP_BAR;
    public float UpdateSpeed = 5.0f;
    public void UpdateHpbar(float currentHp, float maxHp)
    {
        if(currentHp <= 0)
        {
            HP_BAR.fillAmount = 0;
            return;
        }
        StartCoroutine(UpdateBar(HP_BAR,currentHp, maxHp));
    }

    public void UpdateMpbar(float currentMp, float maxMp)
    {
        if (currentMp <= 0)
        {
            MP_BAR.fillAmount = 0;
            return;
        }
        StartCoroutine(UpdateBar(MP_BAR, currentMp, maxMp));
    }

    public void UpdateUnitFrame(float currentHp, float maxHp, float currentMp, float maxMp)
    {
        UpdateHpbar(currentHp, maxHp);
        UpdateMpbar(currentMp, maxMp);
    }

    IEnumerator UpdateBar(Image bar, float currentValue, float maxValue)
    {
        float value = currentValue / maxValue;
        float currentFill = bar.fillAmount;
        float time = 0;
        while (time <= 1.0f)
        {
            time += Time.deltaTime * UpdateSpeed;
            bar.fillAmount = Mathf.Lerp(currentFill, value, time);
            yield return null;
        }
    }

}
