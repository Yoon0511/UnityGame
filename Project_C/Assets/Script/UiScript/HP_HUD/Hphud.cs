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

    float MaxHp = 100f;
    float hp_1 = 100f;
    float hp_2 = 100f;

    Color CurrColor = Color.blue;
    Color NextColor = Color.blue;

    float CurrHp = 0.0f;
    float DivisionHp = 0.0f;
    float MaxDivisionHp = 0.0f;
    int MaxCount = 0;
    int Count = 0;
    private void Start()
    {
        //테스트
        //SetTarget(Shared.GameMgr.PLAYER);
        //테스트
    }

    public void SetTarget(Character _target)
    {
        MaxHp = _target.GetInStatData(STAT_TYPE.MAXHP);
        CurrHp = _target.GetInStatData(STAT_TYPE.HP);
        NAMETEXT.text = _target.GetCharacterName();

        MaxCount = 10;

        LINECOUNT.text = "x" + MaxCount.ToString();

        DivisionHp = MaxHp / (float)MaxCount;
        MaxDivisionHp = DivisionHp;
        //StartCoroutine(ITest());
    }

    IEnumerator ITest()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            DivisionHp -= 3;
            StartCoroutine(IHphud());
        }
    }

    IEnumerator IHphud()
    {
        float value = DivisionHp / MaxDivisionHp;
        float currentFill = CURRHP.fillAmount;
        float time = 0;
        while (time <= 1.0f)
        {
            time += Time.deltaTime * 5.0f;
            CURRHP.fillAmount = Mathf.Lerp(currentFill, value, time);
            yield return null;
        }

        if(DivisionHp <= 0)
        {
            DivisionHp = MaxDivisionHp;
            Count++;
            int LintCount = MaxCount - Count;
            LINECOUNT.text = "x" + LintCount.ToString();

            ChageColor();
        }
    }

    void UpdateHp()
    {
        CURRHP.fillAmount = hp_1 / MaxHp;
        NEXTHP.fillAmount = hp_2 / MaxHp;
    }

    void ChageColor()
    {
        CurrColor = NextColor;
        CURRHP.color = CurrColor;

        CURRHP.fillAmount = 1.0f;

        NextColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        NEXTHP.color = NextColor;
    }
}
