using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SimpleJSON;
using UnityEngine;

public partial class GameMgr
{
    string folderPath;
    string filePath;

    void SavePathInit()
    {
        folderPath = Application.dataPath + "/SaveFolder";
        filePath = folderPath + "/PlayerData.json";
    }
    public void Save()
    {
        Debug.Log("Save");
        string json = JsonUtility.ToJson(PLAYER.SaveData());
        CreateJsonFile(Application.dataPath, "PlayerData", json);
    }

    public void Load()
    {
        Debug.Log("Load");
        string fullPath = string.Format("{0}/{1}.json", folderPath, "PlayerData");
        if (File.Exists(fullPath))
        {
            SaveData SaveData = LoadJsonFile<SaveData>(folderPath, "PlayerData");
            PLAYER.Load(SaveData);
            LoadDataCompleteQuest(SaveData.CompletedQuestData);
        }
        else
        {
            Debug.Log("세이브파일이 없습니다.");
        }
    }

    public void LoadDataCompleteQuest(CompletedQuestJson _data)
    {
        Debug.Log(_data.ListCompletedQuestId.Count);
        for (int i = 0; i < _data.ListCompletedQuestId.Count; ++i)
        {
            for(int j = 0; j < Shared.GameMgr.GetListQuest().Count; ++j)
            {
                if (Shared.GameMgr.GetListQuest()[j].GetId() == _data.ListCompletedQuestId[i])
                {
                    Shared.GameMgr.GetListQuest()[j].SetIsComplete(true);
                    Shared.GameMgr.GetListQuest()[j].StateChange(QUEST_STATE.END);
                }
            }
        }

        for (int j = 0; j < Shared.GameMgr.GetListQuest().Count; ++j)
        {
            Debug.Log($"퀘스트 아이디: {Shared.GameMgr.GetListQuest()[j].GetId()}, 완료 여부: {Shared.GameMgr.GetListQuest()[j].GetIsComplete()}");
        }
    }

    void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        folderPath = createPath + "/SaveFolder";
        filePath = folderPath + "/SaveData.json";

        // 폴더가 없으면 생성
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", folderPath, fileName), FileMode.Create); 
        byte[] data = Encoding.UTF8.GetBytes(jsonData); 
        fileStream.Write(data, 0, data.Length);
        fileStream.Close(); 
    }

    T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length); fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }
}
