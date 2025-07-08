using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public FadeInOut FadeInOut;
    AsyncOperation AsyncOper;
    public ConnectPopup ConnectPopup;
    private void Start()
    {
        FadeInOut.SetFadeDuration(5.0f);
        FadeInOut.FadeIn();
    }

    public void OnGameStart(bool _load)
    {
        ConnectPopup.gameObject.SetActive(true);
        ConnectPopup.StartProgress();
        AsyncOper = SceneManager.LoadSceneAsync((int)SCENE.INGAME);
        SetallowSceneActivation(false);

        Shared.SceneMgr.SetIsDataLoad(_load);
    }

    public void SetallowSceneActivation(bool _value)
    {
        AsyncOper.allowSceneActivation = _value;
    }
}
