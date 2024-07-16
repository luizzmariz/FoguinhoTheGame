using UnityEngine;

[System.Serializable]
public struct Dialogue
{
    public bool hasBeenSpoke;
    public int relationLevelRequired;
    public DayPhase dayPhaseRequired;
    public Sprite dialogueSprite;
	[TextArea(3, 10)] public string[] interactionSentences;
    public bool hasBeenSpokeInThisInteraction;
    [TextArea(3, 10)] public string[] posInteractionSentences;
}

[System.Serializable]
public class NPCData
{
    public string npcName;
    public int relationLevel;
    public Vector3[] positions;
    public Dialogue[] dialogues;

    public NPCData(NPC npc)
    {
        this.npcName = npc.npcName;
        this.positions = npc.positions;
        this.relationLevel = npc.relationLevel;
        this.dialogues = npc.dialogues;
    }
}
