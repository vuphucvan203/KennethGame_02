using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;

[CustomEditor(typeof(DataSystem), true)]
public class CharacterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DataSystem dataSystem = (DataSystem)target;

        EditorGUILayout.LabelField("Create data", EditorStyles.boldLabel);
        dataSystem.type = (CharacterType)EditorGUILayout.EnumPopup("Character", dataSystem.type);
        switch (dataSystem.type)
        {
            case CharacterType.Jack:
            case CharacterType.Linda:
                dataSystem.soldierData.type = dataSystem.type;
                dataSystem.soldierData.name = dataSystem.type.ToString();
                dataSystem.soldierData.level = EditorGUILayout.IntField("Level", dataSystem.soldierData.level);
                dataSystem.soldierData.experience = EditorGUILayout.IntField("Experience", dataSystem.soldierData.experience);
                dataSystem.soldierData.health = EditorGUILayout.IntField("Health", dataSystem.soldierData.health);
                dataSystem.soldierData.attack = EditorGUILayout.IntField("Attack", dataSystem.soldierData.attack);
                dataSystem.soldierData.defense = EditorGUILayout.IntField("Defense", dataSystem.soldierData.defense);
                dataSystem.soldierData.speed = EditorGUILayout.IntField("Speed", dataSystem.soldierData.speed);
                if (GUILayout.Button("Save")) SaveFile(dataSystem.soldierData);
                if (GUILayout.Button("Load")) LoadSoliderData(dataSystem);
                break;
            case CharacterType.MindlessZombie:
            case CharacterType.CopZombie:
            case CharacterType.ArmyZombie:
            case CharacterType.AcidSpitter:
            case CharacterType.FleshThrower:
            case CharacterType.AlphaBeast:
                dataSystem.enemydata.type = dataSystem.type;
                dataSystem.enemydata.name = dataSystem.type.ToString();
                dataSystem.enemydata.health = EditorGUILayout.IntField("Health", dataSystem.enemydata.health);
                dataSystem.enemydata.attack = EditorGUILayout.IntField("Attack", dataSystem.enemydata.attack);
                dataSystem.enemydata.defense = EditorGUILayout.IntField("Defense", dataSystem.enemydata.defense);
                dataSystem.enemydata.speed = EditorGUILayout.IntField("Speed", dataSystem.enemydata.speed);
                if (GUILayout.Button("Save")) SaveFile(dataSystem.enemydata);
                if (GUILayout.Button("Load")) LoadEnemyData(dataSystem);
                break;
            default:
                base.OnInspectorGUI();
                break;
        }    
        if (GUILayout.Button("Delete")) DeleteFile();
    }

    protected string LoadFile(CharacterData data)
    {
        string pathDefault = Application.persistentDataPath;
        string path = EditorUtility.OpenFilePanel("Select file", pathDefault, "json");

        if (!string.IsNullOrEmpty(path))
        {
            string json = File.ReadAllText(path);
            return json;
        }
        return null;
    }

    protected void LoadSoliderData(DataSystem system)
    {
        string json = LoadFile(system.soldierData);
        SoldierData data = JsonUtility.FromJson<SoldierData>(json);
        system.type = data.type;
        system.soldierData.type = data.type;
        system.soldierData.name = data.name;
        system.soldierData.level = data.level;
        system.soldierData.experience = data.experience;
        system.soldierData.health = data.health;
        system.soldierData.attack = data.attack;
        system.soldierData.defense = data.defense;
        system.soldierData.speed = data.speed;
    }

    protected void LoadEnemyData(DataSystem system)
    {
        string json = LoadFile(system.enemydata);
        EnemyData data = JsonUtility.FromJson<EnemyData>(json);
        system.type = data.type;
        system.enemydata.type = data.type;
        system.enemydata.name = data.name;
        system.enemydata.health = data.health;
        system.enemydata.attack = data.attack;
        system.enemydata.defense = data.defense;
        system.enemydata.speed = data.speed;
    }

    protected void SaveFile(CharacterData data)
    {
        string defaultPath = Application.persistentDataPath;
        string path = EditorUtility.SaveFilePanel("Save file", defaultPath, "Data", "json");

        if (!string.IsNullOrEmpty(path))
        {
            string fileName = Path.GetFileName(path);
            DataSystem.SaveData(data, fileName);
        }
    }

    protected void DeleteFile()
    {
        string defaultPath = Application.persistentDataPath;
        string path = EditorUtility.OpenFilePanel("Delete file", defaultPath, "json");

        if (!string.IsNullOrEmpty(path) && File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Success to delete file!");
        }
        else
        {
            Debug.LogWarning("File not found!");
        }
    }
}
