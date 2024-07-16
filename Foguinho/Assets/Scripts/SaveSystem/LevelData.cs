using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct LevelTransitionData
{
    public string transitionName;
    public bool needActivation;
}

[System.Serializable]
public struct LevelPartData
{
    public string levelPartName;
    // public string levelPartNumber;
    public int timeSpentInLevel;
    // public LevelTransitionData[] levelTransitionsData;
    // public NPCData[] npcsData;
    public List<LevelTransitionData> levelTransitionsData;
    public List<NPCData> npcsData;
}

[System.Serializable]
public class LevelData
{
    public string levelName;
    public string levelNumber;
    // public LevelPartData[] levelPartsData;
    public List<LevelPartData> levelPartsData;
    public LevelPartData currentLevelPart;
    

    public LevelData(LevelManager level)
    {
        this.levelName = level.levelName;
        this.levelNumber = level.levelNumber;
        // this.levelPartsData = new LevelPartData[1];
        this.levelPartsData = new List<LevelPartData>();

        if(level.currentLevelPart != null)
        {
            LevelPartData levelPartData = new LevelPartData();

            levelPartData.levelPartName = level.currentLevelPart.levelPartName;
            levelPartData.timeSpentInLevel = level.currentLevelPart.timeSpentInLevel;

            // int index = 0;

            // levelPartData.levelTransitionsData = new LevelTransitionData[level.currentLevelPart.levelTransitions.Count];
            levelPartData.levelTransitionsData = new List<LevelTransitionData>();
            foreach(LevelPartLoader levelTransition in level.currentLevelPart.levelTransitions)
            {
                LevelTransitionData levelTransitionData;

                levelTransitionData.transitionName = levelTransition.transitionName;
                levelTransitionData.needActivation = levelTransition.needActivation;

                // levelPartData.levelTransitionsData[index] = levelTransitionData;
                levelPartData.levelTransitionsData.Add(levelTransitionData);

                // index++;
            }

            // index = 0;

            // levelPartData.npcsData= new NPCData[level.currentLevelPart.npcs.Count];
            levelPartData.npcsData= new List<NPCData>();
            foreach(NPC npc in level.currentLevelPart.npcs)
            {
                NPCData npcData = new NPCData(npc);

                // levelPartData.npcsData[index] = npcData;
                levelPartData.npcsData.Add(npcData);

                // index++;
            }
            
            this.currentLevelPart = levelPartData;
            // this.levelPartsData[0] = levelPartData;
            this.levelPartsData.Add(levelPartData);
        }
    }

    public void SaveNewInfo(LevelManager level)
    {
        if(level.currentLevelPart != null)
        {
            LevelPartData levelPartData = new LevelPartData();

            levelPartData.levelPartName = level.currentLevelPart.levelPartName;
            levelPartData.timeSpentInLevel = level.currentLevelPart.timeSpentInLevel;

            // int index = 0;

            // levelPartData.levelTransitionsData = new LevelTransitionData[level.currentLevelPart.levelTransitions.Count];
            levelPartData.levelTransitionsData = new List<LevelTransitionData>();
            foreach(LevelPartLoader levelTransition in level.currentLevelPart.levelTransitions)
            {
                LevelTransitionData levelTransitionData;

                levelTransitionData.transitionName = levelTransition.transitionName;
                levelTransitionData.needActivation = levelTransition.needActivation;

                // levelPartData.levelTransitionsData[index] = levelTransitionData;
                levelPartData.levelTransitionsData.Add(levelTransitionData);

                // index++;
            }

            // index = 0;

            // levelPartData.npcsData= new NPCData[level.currentLevelPart.npcs.Count];
            levelPartData.npcsData= new List<NPCData>();
            foreach(NPC npc in level.currentLevelPart.npcs)
            {
                NPCData npcData = new NPCData(npc);

                // levelPartData.npcsData[index] = npcData;
                levelPartData.npcsData.Add(npcData);

                // index++;
            }
            
            // index = 0;

            if(levelPartsData != null)
            {
                // bool hasBeenLoaded = false;

                foreach(LevelPartData PartData in levelPartsData.ToList())
                {
                    if(PartData.levelPartName == level.currentLevelPart.levelPartName)
                    {
                        // levelPartsData[index] = levelPartData;
                        levelPartsData.Remove(levelPartData);
                        // hasBeenLoaded = true;
                    }

                    // index++;
                }

                // if(!hasBeenLoaded)
                // {
                    levelPartsData.Add(levelPartData);
                // }
            }
            else
            {
                // levelPartsData = new LevelPartData[1];
                // levelPartsData[index] = levelPartData;

                this.levelPartsData = new List<LevelPartData>();
                levelPartsData.Add(levelPartData);
            }

            this.currentLevelPart = levelPartData;
        }
    }
}