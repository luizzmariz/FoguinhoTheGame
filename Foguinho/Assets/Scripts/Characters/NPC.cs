using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour
{
    public string npcName;
    public Vector3[] positions;
    public int relationLevel;
    public Dialogue[] dialogues;
    
    public void WalkToNewPosition()
    {
        DayPhase dayPhase = DayCycleManager.instance.dayPhase;

        if(dayPhase == DayPhase.MORNING)
        {
            transform.position = positions[0];
        }
        else if(dayPhase == DayPhase.AFTERNOON)
        {
            transform.position = positions[1];
        }
        else if(dayPhase == DayPhase.EVENING)
        {
            transform.position = positions[2];
        }
        else if(dayPhase == DayPhase.NIGHT)
        {
            transform.position = positions[3];
        }
    }

    public void LoadNPCData(List<NPCData> npcsData)
    {
        // NPCData npcData = SaveSystem.LoadNPC(this);

        foreach(NPCData npcData in npcsData)
        {
            if(npcData.npcName == this.npcName)
            {
                this.positions = npcData.positions;
                this.relationLevel = npcData.relationLevel;
                this.dialogues = npcData.dialogues;
            }
        }
    }

    public void SaveNPCData()
    {
        SaveSystem.SaveNPC(this);
    } 
}
