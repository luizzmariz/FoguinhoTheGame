using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    static string worldDataPath = Application.persistentDataPath + "/" + "World" + ".json";
    static string playerDataPath = Application.persistentDataPath + "/" + "Player" + ".json";

    public static void CreateSave()
    {
        WorldData data = new WorldData();

        string worldData = JsonUtility.ToJson(data);

        File.WriteAllText(worldDataPath, worldData);
    }

    public static void DeleteSave()
    {
        if(File.Exists(worldDataPath))
        {
            File.Delete(worldDataPath);
        }

        if(File.Exists(playerDataPath))
        {
            File.Delete(playerDataPath);
        }
    }

    public static void SaveGame()
    {
        WorldData data = LoadGame();

        if(data != null)
        {
            data.SaveNewInfo(GameManager.instance.currentLevel);
        }
        else
        {
            Debug.Log("Error saving the game");
        }

        string worldData = JsonUtility.ToJson(data);

        File.WriteAllText(worldDataPath, worldData);
    }

    public static WorldData LoadGame()
    {
        string worldData;

        if(File.Exists(worldDataPath))
        {
            worldData = File.ReadAllText(worldDataPath);

            return JsonUtility.FromJson<WorldData>(worldData);
        }
        else
        {
            return null;
        }
    }
    
    public static void SaveNPC(NPC npc)
    {
        NPCData data = new NPCData(npc);

        string npcData = JsonUtility.ToJson(data);
        string filePath = Application.persistentDataPath + "/" + npc.npcName + ".json";

        System.IO.File.WriteAllText(filePath, npcData);
    }

    public static NPCData LoadNPC(NPC npc)
    {
        string filePath = Application.persistentDataPath + "/" + npc.npcName + ".json";
        string npcData = System.IO.File.ReadAllText(filePath);

        return JsonUtility.FromJson<NPCData>(npcData);
    }

    public static LevelData SaveLevel(LevelManager level)
    {
        WorldData data = LoadGame();

        if(data != null)
        {
            data.SaveNewInfo(level);
        }

        string worldData = JsonUtility.ToJson(data);

        File.WriteAllText(worldDataPath, worldData);

        return data.currentLevel;
    }

    public static LevelData LoadLevel(LevelManager level)
    {
        WorldData data = LoadGame();
        LevelData levelData = null;

        bool hasDataToLoad = false;

        if(data != null)
        {
            if(!(data.levelsData == null || data.levelsData.Count == 0))
            {
                foreach(LevelData lvlData in data.levelsData)
                {
                    if(lvlData.levelNumber == level.levelNumber)
                    {
                        hasDataToLoad = true;
                        levelData = lvlData;
                    }
                }
            }
        }
        else
        {
            CreateSave();
        }

        if(!hasDataToLoad)
        {
            levelData = SaveLevel(level);
        }

        return levelData;
    }
}
