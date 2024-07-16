using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : Interactable
{
    public String NPCName;
    public int actionCost;

    // void Start()
    // {
    //     GameObject.Find("LevelManager").GetComponent<DialogueManager>().StartDialogue(dialogue, dialogueSprite);
    // }

    protected override void Interact()
    {
        LevelManager.instance.StartSpeakerDialogue(NPCName,actionCost);
    }

    public override string GetPromptMessage()
    {
        return "talk with " + promptMessage;
    }
}
