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
    float DisappearElaspedTime = 0.0f; // 사라지는 시간 초기화용

    Color CurrColor = Color.red;
    Color NextColor = Color.red;

    List<Color> UsedColors = new List<Color>(); // 이전 색상 기록용

    Character Target;
    Coroutine testCoroutine;

    [SerializeField]
    BuffUi BuffUi;

    bool IsShow = false;
    public GameObject HP_HUDUI;
    private void Start()
    {
        ChangeColor();
        HP_HUDUI.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 180f);
    }

    public void SetTarget(Character _target)
    {
        DisappearElaspedTime = 0.0f;
        Target = _target;

        if (Target == null)
            return;

        MaxHp = Target.GetInStatData(STAT_TYPE.MAXHP);
        CurrHp = Target.GetInStatData(STAT_TYPE.HP);
        previousHp = CurrHp;
        ChangeColor();

        NAMETEXT.text = Target.GetCharacterName();
        MaxCount = 10;
        HPTEXT.text = $"<color=#FFFAFA><b>{CurrHp.ToString("F0")} / {MaxHp.ToString("F0")}</b></color>";

        MaxDivisionHp = MaxHp / MaxCount;

        // 현재 체력 상태에 맞게 Count, DivisionHp 계산
        Count = Mathf.FloorToInt((MaxHp - CurrHp) / MaxDivisionHp);
        DivisionHp = MaxDivisionHp - ((MaxHp - CurrHp) % MaxDivisionHp);
        DivisionHp = Mathf.Clamp(DivisionHp, 0, MaxDivisionHp);

        int leftCount = MaxCount - Count;
        LINECOUNT.text = "x" + leftCount.ToString();

        // 색상 설정 및 초기화
        if(DivisionHp == MaxDivisionHp) // 풀피일 경우 색상 초기화
        {
            UsedColors.Clear();
            //ChangeColor();
            for (int i = 0; i < Count; i++)
            {
                UsedColors.Add(CurrColor);
            }
            CURRHP.color = CurrColor;
            NEXTHP.color = NextColor;
        }
       
        // 현재 체력에 맞게 설정
        CURRHP.fillAmount = DivisionHp / MaxDivisionHp;

        // 현재 체력 반영
        StartCoroutine(IHphud());

        if (testCoroutine != null)
            StopCoroutine(testCoroutine);

        // 현재 타겟이 보유한 버프 정보 갱신
        BuffUi.SetTarget(Target);

        IsShow = true;
        HP_HUDUI.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
        testCoroutine = StartCoroutine(IUpdateHpHud());
    }

    public void ShowHud(Character _target)
    {
        if (IsShow == false)
        {
            DisappearElaspedTime = 0.0f;
            SetTarget(_target);
        }
        else //HP HUD가 이미 보이는 상태라면
        {
            DisappearElaspedTime = 0.0f; //사라지는 시간 초기화
        }
    }
    IEnumerator IUpdateHpHud()
    {
        while (Target != null)
        {
            //yield return new WaitForSeconds(0.1f);
            yield return null;

            // 현재 타겟이 가지고 있는 실시간 버프 정보 갱신
            BuffUi.UpdateBuffUi(Target);

            float currentHp = Target.GetInStatData(STAT_TYPE.HP);
            float diff = currentHp - previousHp;
            CurrHp = currentHp;
            previousHp = currentHp;

            //HPTEXT.text = CurrHp.ToString("F0") + "/" + MaxHp.ToString("F0");
            HPTEXT.text = $"<color=#FFFAFA><b>{CurrHp.ToString("F0")} / {MaxHp.ToString("F0")}</b></color>";

            if (IsShow)
            {
                DisappearElaspedTime += Time.deltaTime;
                if(DisappearElaspedTime >= 3.0f)
                {
                    IsShow = false;
                    HP_HUDUI.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 180f);
                }
            }

            if(CurrHp <= 0 && IsShow)
            {
                IsShow = false;
                HP_HUDUI.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 180f);
            }

            if (Mathf.Abs(diff) < 0.01f) continue;

            DisappearElaspedTime = 0.0f;

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
        float time = 0f;
        float startFill = CURRHP.fillAmount;

        while (time <= 1f)
        {
            time += Time.deltaTime * 5.0f;
            CURRHP.fillAmount = Mathf.Lerp(startFill, value, time);
            yield return null;
        }

        CURRHP.fillAmount = value;
    }

    void ChangeColor()
    {
        CurrColor = NextColor;
        CURRHP.color = CurrColor;
        
        // HP 비율 (0 = 0%, 1 = 100%)
        float hpRatio = CurrHp / MaxHp;

        // 빨강(1,0,0) → 파랑(0,0,1)로 선형 보간
        //CurrColor = Color.Lerp(Color.blue, Color.red, hpRatio);
        //CURRHP.color = CurrColor;

        // 다음 색상도 같은 방식으로 설정
        NextColor = Color.Lerp(Color.blue, Color.red, hpRatio);
        //NEXTHP.color = NextColor;

        CURRHP.fillAmount = 1.0f;

        // 새로운 랜덤 색상
        //NextColor = new Color(Random.Range(0.7f, 1f), 0.3f,0.3f);
        NEXTHP.color = NextColor;
    }
}
