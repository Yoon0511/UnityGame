using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitFrame : MonoBehaviour
{
    public Image HP_BAR;
    public Image MP_BAR;
    public float UpdateSpeed = 5.0f;

    public Text HPTEXT;
    public Text MPTEXT;

    public void UpdateHpbar(float _currentHp, float _maxHp)
    {
        if(_currentHp <= 0)
        {
            HP_BAR.fillAmount = 0;
            return;
        }

        HPTEXT.text = _currentHp.ToString() + "/" + _maxHp.ToString();
        StartCoroutine(IUpdateBar(HP_BAR, _currentHp, _maxHp));
    }

    public void UpdateMpbar(float _currentHp, float _maxHp)
    {
        if (_currentHp <= 0)
        {
            MP_BAR.fillAmount = 0;
            return;
        }

        MPTEXT.text = _currentHp.ToString() + "/" + _maxHp.ToString();
        StartCoroutine(IUpdateBar(MP_BAR, _currentHp, _maxHp));
    }

    public void UpdateUnitFrame(float currentHp, float maxHp, float currentMp, float maxMp)
    {
        UpdateHpbar(currentHp, maxHp);
        UpdateMpbar(currentMp, maxMp);
    }

    IEnumerator IUpdateBar(Image bar, float currentValue, float maxValue)
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
