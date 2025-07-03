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
    //string[] Villgename = { "Oakridge", "Redhill", "Brimford", "Greenbrook" };

    public Store Store;
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
