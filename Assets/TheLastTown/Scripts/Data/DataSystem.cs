using System.IO;
using UnityEngine;

public class DataSystem : MonoBehaviour
{
    public CharacterType type;
    public string fileName;
    public SoldierData soldierData;
    public EnemyData enemydata;

    public static void SaveData(CharacterData data, string fileName)
    {
        string json = JsonUtility.ToJson(data, true);
        string path = Application.persistentDataPath + "/" + fileName;
        File.WriteAllText(path, json);
        Debug.Log("Success to save file!");
    }    

    protected static string LoadData(string file)
    {
        string path = Application.persistentDataPath + "/" + file;
        string json = File.ReadAllText(path);
        if(File.Exists(path))
        {
            Debug.Log("Success to load file!");
            return json;
        }
        else
        {
            Debug.Log("Failed to load file!");
            return null;
        } 
    }    

    public static SoldierData LoadSoldierData(string file)
    {
        string json = LoadData(file);
        SoldierData data = JsonUtility.FromJson<SoldierData>(json);
        return data;
    }

    public static EnemyData LoadEnemyData(string file)
    {
        string json = LoadData(file);
        EnemyData data = JsonUtility.FromJson<EnemyData>(json);
        return data;
    }
}
