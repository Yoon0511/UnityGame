using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public class ServerMgr : MonoBehaviour
{
    string Http = "http://127.0.0.1:3000/";

    string ConnectUrl = "process/shopreset";
    private void Awake()
    {
        Shared.ServerMgr = this;
        DontDestroyOnLoad(this);
    }
    IEnumerator DBPost(string _Url, string _Num)
    {
        WWWForm form = new WWWForm();
        form.AddField("num", _Num);

        UnityWebRequest www = UnityWebRequest.Post(_Url, form);
        //UnityWebRequest www = UnityWebRequest.Get(_Url);

        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);

        JSONNode node = JSONNode.Parse(www.downloadHandler.text);

        string str = node;
        Debug.Log(str);
    }
    IEnumerator DBGet(string _Url, string _Num)
    {
        WWWForm form = new WWWForm();
        form.AddField("num", _Num);

        //UnityWebRequest www = UnityWebRequest.Post(_Url, form);
        UnityWebRequest www = UnityWebRequest.Get(_Url);

        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);

        JSONNode node = JSONNode.Parse(www.downloadHandler.text);

        string str = node;

        Debug.Log(node[0]);
    }

    public void OnBtnConnect()
    {
        //StartCoroutine(DBPost(Http + ConnectUrl,"dev"));
        StartCoroutine(DBGet(Http + ConnectUrl, "dev"));
    }
    IEnumerator DBGet(string _Url, string _Num, Action<JSONNode> _callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(_Url);

        yield return www.SendWebRequest();

        //Debug.Log(www.downloadHandler.text);

        JSONNode node = JSONNode.Parse(www.downloadHandler.text);

        string str = node;

        //Debug.Log(node[0]);

        _callback?.Invoke(node);
    }

    public void 
        OnBtnConnect(Action<JSONNode> _callback)
    {
        StartCoroutine(DBGet(Http + ConnectUrl, "dev", _callback));
    }
}
