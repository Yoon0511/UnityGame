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

    List<Color> UsedColors = new List<Color>(); // 이전 색상 기록용

    Character Target;
    Coroutine testCoroutine;

    private void Start()
    {
        // 테스트용
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

        Count = 0;
        UsedColors.Clear();

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
            float diff = currentHp - previousHp;
            CurrHp = currentHp;
            previousHp = currentHp;

            HPTEXT.text = CurrHp.ToString("F0") + "/" + MaxHp.ToString("F0");

            if (Mathf.Abs(diff) < 0.01f) continue;

            if (diff < 0)
            {
                // 데미지 처리
                float dmg = -diff;
                while (dmg > 0 && Count < MaxCount)
                {
                    if (dmg >= DivisionHp)
                    {
                        dmg -= DivisionHp;
                        DivisionHp = MaxDivisionHp;
                        Count++;

                        UsedColors.Add(CurrColor); // 현재 색상 저장
                        ChangeColor();

                        int leftCount = MaxCount - Count;
                        LINECOUNT.text = "x" + leftCount.ToString();
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
            else
            {
                // 회복 처리
                float heal = diff;
                while (heal > 0 && Count >= 0)
                {
                    float remain = MaxDivisionHp - DivisionHp;
                    if (heal >= remain)
                    {
                        heal -= remain;
                        DivisionHp = 0f;

                        if (Count > 0)
                        {
                            Count--;
                            DivisionHp = MaxDivisionHp;

                            if (UsedColors.Count > 0)
                            {
                                NextColor = CURRHP.color;
                                CurrColor = UsedColors[UsedColors.Count - 1];
                                UsedColors.RemoveAt(UsedColors.Count - 1);
                                CURRHP.color = CurrColor;
                                NEXTHP.color = NextColor;
                            }

                            int leftCount = MaxCount - Count;
                            LINECOUNT.text = "x" + leftCount.ToString();
                        }
                    }
                    else
                    {
                        DivisionHp += heal;
                        DivisionHp = Mathf.Clamp(DivisionHp, 0, MaxDivisionHp);
                        heal = 0;
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

        // 새로운 랜덤 색상
        NextColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        NEXTHP.color = NextColor;
    }
}
