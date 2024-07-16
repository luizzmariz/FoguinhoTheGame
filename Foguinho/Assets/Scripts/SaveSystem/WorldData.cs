using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class WorldData
{
    public DayPhase currentDayPhase;
    public List<LevelData> levelsData;
    public LevelData currentLevel;
    

    public WorldData()
    {
        currentDayPhase = DayPhase.MORNING;
        this.levelsData = new List<LevelData>();
    }

    public void SaveNewInfo(LevelManager level)
    {
        currentDayPhase = DayCycleManager.instance.dayPhase;

        if(levelsData != null)
        {
            LevelData data = new LevelData(level);

            foreach(LevelData levelData in levelsData.ToList())
            {
                if(levelData.levelNumber == level.levelNumber)
                {
                    levelsData.Remove(levelData);
                }
            }

            levelsData.Add(data);

            this.currentLevel = data;
        }
    }
}
