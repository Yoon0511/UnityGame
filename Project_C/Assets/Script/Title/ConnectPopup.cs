using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectPopup : MonoBehaviour
{
    public Title Title;
    [SerializeField]
    Image ProgressBar;

    [SerializeField]
    Text Text;

    [SerializeField]
    float Duration;
    float Progress;
    public void StartProgress()
    {
        StartCoroutine(IProgress());
        StartCoroutine(IAnimationText());
    }

    IEnumerator IProgress()
    {
        float time = 0f;

        while (time < Duration)
        {
            float t = time / Duration;
            ProgressBar.fillAmount = t;
            time += Time.deltaTime;

            if(t >= 0.9f)
            {
                yield return new WaitForSeconds(1.0f);
                ChangeScene();
            }
            yield return null;
        }
    }

    IEnumerator IAnimationText()
    {
        int dotCount = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            dotCount = (dotCount + 1) % 4; // 0~3
            Text.text = "서버 접속중" + new string('.', dotCount);
        }
    }
    public bool GetLoadingEnd()
    {
        return Progress >= 1.0f;
    }

    void ChangeScene()
    {
        Title.SetallowSceneActivation(true);
        //Shared.SceneMgr.ChangeScene(SCENE.INGAME);
    }
}
