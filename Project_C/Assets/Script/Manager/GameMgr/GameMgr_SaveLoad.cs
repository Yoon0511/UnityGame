using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        }
        else
        {
            Debug.Log("세이브파일이 없습니다.");
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
