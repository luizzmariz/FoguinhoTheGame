using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class LevelPart : MonoBehaviour
{
    [Header("Level Part")]
    public string levelPartName;
    public bool shouldContactLM;
    public List<LevelPartLoader> levelTransitions = new List<LevelPartLoader>();

    [Header("Interaction System")]
    public List<NPC> npcs = new List<NPC>();
    public int timeSpentInLevel;
    public int actionTokensGap;
    bool actionCostApply;
    int lastActionCost;

    [Header("Player")]
    public Vector3 spawnPosition;

    void Start() 
    {
        if(shouldContactLM)
        {
            GetNPCs();

            GetLevelTransitions();
            //spawnPosition = transform.GetChild(0).position;
            RefreshLevelPart();

            //spawnPosition = GameObject.Find("Floor").transform.position;
            // StartCoroutine(ContactLevelManager(3));
            ContactLevelManager();
        }
    }

    void GetNPCs()
    {
        foreach(GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
        {
            npcs.Add(npc.GetComponent<NPC>());
        }
    }

    void GetLevelTransitions()
    {
        foreach(GameObject levelTransition in GameObject.FindGameObjectsWithTag("LevelTransition"))
        {
            levelTransitions.Add(levelTransition.GetComponent<LevelPartLoader>());
        }
    }

    // IEnumerator ContactLevelManager(int g)
    // {
    //     yield return new WaitForSeconds(0.02f);

    //     bool hasLoadedBefore = false;
    //     LevelPart switcher = null;

    //     foreach(LevelPart levelPart in LevelManager.instance.levelParts)
    //     {
    //         if(levelPart.levelPartName == this.levelPartName)
    //         {
    //             hasLoadedBefore = true;
    //             switcher = levelPart;
    //         }
    //     }

    //     if(hasLoadedBefore)
    //     {
    //         this.npcs = switcher.npcs;
    //         this.timeSpentInLevel = switcher.timeSpentInLevel;
    //         // foreach(LevelPartLoader levelPartLoader in levelTransitions)
    //         // {
    //         //     Debug.Log(levelPartLoader.name);
    //         // }
    //         // this.levelTransitions = switcher.levelTransitions;
    //         // foreach(LevelPartLoader levelPartLoader in levelTransitions)
    //         // {
    //         //     Debug.Log(levelPartLoader.name);
    //         // }

    //         //Debug.Log("hehehehe" + " at " + Time.time);
    //     }
    //     else
    //     {
    //         LevelManager.instance.AddLevelPart(this);

    //         //Debug.Log("hohohoho" + " at " + Time.time);
    //     }

    //     LevelManager.instance.currentLevelPart = this;

    //     LevelManager.instance.currentLevelPartIsReady = true;
    // }

    void ContactLevelManager()
    {
        LevelData levelData = SaveSystem.LoadLevel(LevelManager.instance);

        bool hasDataToLoad = false;

        LevelManager.instance.currentLevelPart = this;

        if(levelData.levelPartsData == null)
        {
            SaveSystem.SaveLevel(LevelManager.instance);
        }
        else
        {
            foreach(LevelPartData levelPartData in levelData.levelPartsData)
            {
                if(levelPartData.levelPartName == this.levelPartName)
                {
                    hasDataToLoad = true;

                    this.timeSpentInLevel = levelPartData.timeSpentInLevel;

                    foreach(NPC npc in npcs)
                    {
                        npc.LoadNPCData(levelPartData.npcsData);
                    }

                    foreach(LevelPartLoader levelTransition in levelTransitions)
                    {
                        levelTransition.LoadData(levelPartData.levelTransitionsData);
                    }
                }
            }

            if(!hasDataToLoad)
            {
                SaveSystem.SaveLevel(LevelManager.instance);
            }
        }

        LevelManager.instance.currentLevelPartIsReady = true;
        // this.timeSpentInLevel = levelPartData.timeSpentInLevel;
    }

    // public void AddLevelPartLoader(LevelPartLoader levelPartLoader)
    // {
    //     LevelPartLoader newLevelPartLoader = this.gameObject.AddComponent<LevelPartLoader>();

    //     newLevelPartLoader.partLoaderName = levelPartLoader.partLoaderName;;
    //     newLevelPartLoader.transitionTo = levelPartLoader.transitionTo;
    //     newLevelPartLoader.needActivation = levelPartLoader.needActivation;
    //     newLevelPartLoader.startPosition = levelPartLoader.startPosition;
    //     newLevelPartLoader.referenceLevelPartLoader = levelPartLoader;

    //     levelTransitions.Add(newLevelPartLoader);
    // }

    public void GetSpeakerDialogue(string speakerName, int actionCost)
    {

        actionCostApply = false;

        NPC targetSpeaker = null;

        foreach(NPC speaker in npcs)
        {
            if(speaker.npcName == speakerName)
            {
                targetSpeaker = speaker;
            }   
        }

        string[] speech = null;
        Sprite sprite = null;

        int foreachI = 0;
        if(targetSpeaker != null)
        {
            foreach(Dialogue dialogue in targetSpeaker.dialogues)
            {
                if(!dialogue.hasBeenSpoke)
                {
                    if(dialogue.dayPhaseRequired == DayCycleManager.instance.dayPhase)
                    {
                        if(dialogue.relationLevelRequired <= targetSpeaker.relationLevel)
                        {
                            if(!dialogue.hasBeenSpokeInThisInteraction)
                            {
                                actionCostApply = true;
                                lastActionCost = actionCost;
                                speech = dialogue.interactionSentences;
                                targetSpeaker.dialogues[foreachI].hasBeenSpokeInThisInteraction = true;
                            }
                            else
                            {
                                actionCostApply = false;
                                lastActionCost = 0;
                                speech = dialogue.posInteractionSentences;
                            }
                            sprite = dialogue.dialogueSprite;
                        }
                    }
                }
                foreachI++;
            }

            DialogueManager.instance.StartDialogue(speakerName, speech, sprite);
        }
        else
        {
            speech = new string[1];
            speech[0] = "Something went wrong";
            DialogueManager.instance.StartDialogue("???", speech, sprite);
        }
    }

    public void SendActionTokens()
    {
        if(lastActionCost != 0 && actionCostApply)
        {
            DayCycleManager.instance.UpdateActionTokens(lastActionCost);
        }

        lastActionCost = 0;
    }

    // public void SaveAllNPCs()
    // {
    //     Debug.Log(npcs.Count);
    //     foreach(NPC speaker in npcs)
    //     {
    //         speaker.SaveNPCData();  
    //     }
    // }

    // public void LoadAllNPCs()
    // {
    //     foreach(NPC speaker in npcs)
    //     {
    //         speaker.LoadNPCData();  
    //     }
    // }

    public void RefreshLevelPart()
    {
        foreach(NPC speaker in npcs)
        {
            speaker.WalkToNewPosition();  
        }
    }
}
