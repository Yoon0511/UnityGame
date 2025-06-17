using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMgr : MonoBehaviour
{
    SCENE NextScene = SCENE.TITLE;
    public SCENE Scene = SCENE.TITLE;
    public AsyncOperation AsyncOper;
    public Image Image;
    private void Awake()
    {
        Shared.SceneMgr = this;
        DontDestroyOnLoad(this);
    }
    public void ChangeScene(SCENE _e, bool _Loading = false)
    {
        if (Scene == _e)
            return;

        if (_Loading)
        {
            NextScene = _e;
            return;
        }
        else
            Scene = _e;

        switch (_e)
        {
            case SCENE.TITLE:
                SceneManager.LoadScene((int)SCENE.TITLE);
                break;
            case SCENE.LOADING:
                SceneManager.LoadScene((int)SCENE.LOADING);
                break;
            case SCENE.INGAME:
                SceneManager.LoadScene((int)SCENE.INGAME);
                break;
        }
    }

    public void NextChangeScene()
    {
        ChangeScene(NextScene);
    }

    public float GetLoadingProgress()
    {
        return AsyncOper.progress;
    }

 
}
