using UnityEngine;

public class DataManager //deals with saving & loading data.
{
    public static void SaveData(DataBase dataBase)
    {
        string dataString = JsonUtility.ToJson(dataBase);
        PlayerPrefs.SetString("data", dataString);
    }
    public static void LoadData(DataBase dataBase)
    {
        if (!PlayerPrefs.HasKey("data"))
        {
            SaveData(dataBase);
            return;
        }

        string dataString = PlayerPrefs.GetString("data");
        JsonUtility.FromJsonOverwrite(dataString, dataBase);
    }
}
