using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    SCENE NextScene = SCENE.TITLE;
    public SCENE Scene = SCENE.TITLE;

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

            //SceneManager.LoadScene("Loading");

            //SceneManager.LoadSceneAsync()
            return;
        }
        else
            Scene = _e;

        switch (_e)
        {
            case SCENE.TITLE:
                SceneManager.LoadScene("Title");
                break;
            case SCENE.INGAME:
                SceneManager.LoadScene("Title");
                break;
        }
    }

    public void NextChangeScene()
    {
        ChangeScene(NextScene);
    }
}
