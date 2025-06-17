using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField]
    Image ProgressImg;

    public float Progress;

    public SceneMgr SceneMgr;

    private void Start()
    {
        SceneMgr = Shared.SceneMgr;
        Debug.Log(SceneMgr);
        //Progress = SceneMgr.GetLoadingProgress();
        Progress = SceneMgr.AsyncOper.progress;
        Debug.Log(Progress);
    }
    private void FixedUpdate()
    {
        //if(SceneMgr == null)
        //{
        //    SceneMgr = Shared.SceneMgr;
        //    Debug.Log("123");
        //}
        //else
        //{
        //    Progress = SceneMgr.GetLoadingProgress();
        //    if (ProgressImg != null)
        //    {
        //        ProgressImg.fillAmount = Progress;
        //        if (Progress > 0.9f)
        //        {
        //            SceneMgr.SetallowSceneActivation(true);
        //        }
        //    }
        //}      
    }
}
