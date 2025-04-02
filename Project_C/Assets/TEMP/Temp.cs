using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Temp : MonoBehaviour
{
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


    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        Vector3 pos = Camera.main.WorldToScreenPoint(hit.point);
        //        Shared.GameMgr.DAMAGEIMAGETEXT.CreateDamageImage(Random.Range(10,9999),pos);
        //    }
        //}
    }
}
