using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : Interactable
{
    public Dialogue dialogue;
    public Sprite dialogueSprite;

    protected override void Interact()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, dialogueSprite);
    }

    public override string GetPromptMessage()
    {
        return "talk with " + promptMessage;
    }
}
