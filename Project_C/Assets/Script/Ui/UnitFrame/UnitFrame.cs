using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitFrame : MonoBehaviour
{
    public Image HP_BAR;
    public Image MP_BAR;
    public Image EXP_BAR;
    public float UpdateSpeed = 5.0f;

    public Text HPTEXT;
    public Text MPTEXT;
    public Text EXPTEXT;
    public Text LEVELTEXT;

    public void UpdateHpbar(float _currenthp, float _maxhp)
    {
        if(_currenthp <= 0)
        {
            HP_BAR.fillAmount = 0;
            return;
        }

        int intcurrentHp = (int)_currenthp;
        HPTEXT.text = intcurrentHp.ToString() + "/" + _maxhp.ToString();
        StartCoroutine(IUpdateBar(HP_BAR, _currenthp, _maxhp));
    }

    public void UpdateMpbar(float _currentmp, float _maxmp)
    {
        if (_currentmp <= 0)
        {
            MP_BAR.fillAmount = 0;
            return;
        }

        int intcurrentMp = (int)_currentmp;
        MPTEXT.text = intcurrentMp.ToString() + "/" + _maxmp.ToString();
        StartCoroutine(IUpdateBar(MP_BAR, _currentmp, _maxmp));
    }

    public void UpdateExpbar(float _currentexp, float _maxexp,float _level)
    {
        int intcurrentExp = (int)_currentexp;
        EXPTEXT.text = _currentexp.ToString() + "%";
        StartCoroutine(IUpdateBar(EXP_BAR, _currentexp, _maxexp));

        LEVELTEXT.text = _level.ToString();
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
