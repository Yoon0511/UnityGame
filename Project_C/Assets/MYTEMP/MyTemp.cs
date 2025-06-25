using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyTemp : MonoBehaviour
{
    public EquipmentEnhancement Enhancement;
    //Æ÷Åæ¼­¹ö
    //string Http = "http://58.78.211.182:3000/";

    //string ConnectUrl = "process/dbconnect";

    //IEnumerator DBPost(string _Url,string _Num)
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("num", _Num);

    //    UnityWebRequest www = UnityWebRequest.Post(_Url, form);

    //    yield return www.SendWebRequest();

    //    Debug.Log(www.downloadHandler.text);

    //    Debug.Log(JSONNode.Parse(www.downloadHandler.text)[0]);
    //    Debug.Log(JSONNode.Parse(www.downloadHandler.text)[0]["account"]);
    //    Debug.Log(JSONNode.Parse(www.downloadHandler.text)[0]["userName"]);
    //}

    //public void OnBtnConnect()
    //{
    //    StartCoroutine(DBPost(Http + ConnectUrl,"dev"));
    //}

    //string[] Villgename = { "Oakridge", "Redhill", "Brimford", "Greenbrook" };

    public Store Store;

    private void Start()
    {
        
    }
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.I))
        //{
        //    Enhancement.gameObject.SetActive(true);
        //    Enhancement.Init();
        //}
        //
        //if(Input.GetKeyDown(KeyCode.O))
        //{
        //    VillageName villageName = Shared.PoolMgr.GetObject("VillageName").GetComponent<VillageName>();
        //    villageName.Init(Villgename[Random.Range(0, Villgename.Length)]);
        //}
        //
        //if(Input.GetKeyDown(KeyCode.P))
        //{
        //    Store.gameObject.SetActive(true);
        //    //
        //    //Store.Init();
        //}
        if (Input.GetKeyDown(KeyCode.P))
        {
            Shared.GameMgr.Save();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Shared.GameMgr.Load();
        }
    }
}
