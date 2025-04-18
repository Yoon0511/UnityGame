using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hphud : MonoBehaviour
{
    public Image CURRHP;
    public Image NEXTHP;
    public Text LINECOUNT;
    public Text NAMETEXT;
    public Text HPTEXT;

    float MaxHp = 100f;
    float CurrHp = 0.0f;
    float DivisionHp = 0.0f;
    float MaxDivisionHp = 0.0f;
    int MaxCount = 0;
    int Count = 0;
    float previousHp = 0.0f;

    Color CurrColor = Color.blue;
    Color NextColor = Color.blue;

    Character Target;
    Coroutine testCoroutine;

    private void Start()
    {
        // 테스트용 - 실제 게임에서 SetTarget 호출 필요
        // SetTarget(Shared.GameMgr.PLAYER);
    }

    public void SetTarget(Character _target)
    {
        Target = _target;

        if (Target == null)
            return;

        MaxHp = Target.GetInStatData(STAT_TYPE.MAXHP);
        CurrHp = Target.GetInStatData(STAT_TYPE.HP);
        previousHp = CurrHp;

        NAMETEXT.text = Target.GetCharacterName();
        MaxCount = 10;
        LINECOUNT.text = "x" + MaxCount.ToString();
        HPTEXT.text = CurrHp.ToString() + "/" + MaxHp.ToString();

        DivisionHp = MaxHp / (float)MaxCount;
        MaxDivisionHp = DivisionHp;

        CURRHP.color = CurrColor;
        NEXTHP.color = NextColor;

        if (testCoroutine != null)
            StopCoroutine(testCoroutine);

        testCoroutine = StartCoroutine(ITest());
    }

    IEnumerator ITest()
    {
        while (Target != null)
        {
            yield return new WaitForSeconds(0.1f);

            float currentHp = Target.GetInStatData(STAT_TYPE.HP);

            float dmg = previousHp - currentHp;
            previousHp = currentHp;

            CurrHp = currentHp;
            HPTEXT.text = CurrHp.ToString("F0") + "/" + MaxHp.ToString("F0");

            if (dmg > 0)
            {
                while (dmg > 0 && Count < MaxCount)
                {
                    if (dmg >= DivisionHp)
                    {
                        dmg -= DivisionHp;
                        DivisionHp = MaxDivisionHp;
                        Count++;

                        int leftCount = MaxCount - Count;
                        LINECOUNT.text = "x" + leftCount.ToString();
                        
                        ChangeColor();
                    }
                    else
                    {
                        DivisionHp -= dmg;
                        DivisionHp = Mathf.Clamp(DivisionHp, 0, MaxDivisionHp);
                        dmg = 0;
                    }

                    yield return StartCoroutine(IHphud());
                }
            }
        }
    }

    IEnumerator IHphud()
    {
        float value = DivisionHp / MaxDivisionHp;
        float currentFill = CURRHP.fillAmount;
        float time = 0;

        while (time <= 0.5f)
        {
            time += Time.deltaTime * 5.0f;
            CURRHP.fillAmount = Mathf.Lerp(currentFill, value, time);
            yield return null;
        }
    }

    void ChangeColor()
    {
        CurrColor = NextColor;
        CURRHP.color = CurrColor;
        CURRHP.fillAmount = 1.0f;

        // 새로운 색상 생성
        NextColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        NEXTHP.color = NextColor;
    }
}