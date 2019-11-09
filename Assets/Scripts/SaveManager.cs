using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveManager : Singleton<SaveManager>
{
    public static void SaveData(string filename, SaveData data)
    {
        string jsonData = JsonConvert.SerializeObject(data);

        File.WriteAllText(Application.persistentDataPath + filename + ".json", jsonData);
    }

    public static SaveData LoadData(string filename)
    {
        if (File.Exists(Application.persistentDataPath + filename + ".json"))
        {
            string jsonData = File.ReadAllText(Application.persistentDataPath + filename + ".json");
            return JsonConvert.DeserializeObject<SaveData>(jsonData);
        }

        return new SaveData();
    }

    public static void DeleteSave(string filename)
    {
        if (File.Exists(Application.persistentDataPath + filename + ".json"))
        {
            File.Delete(Application.persistentDataPath + filename + ".json");
        }
    }
}
