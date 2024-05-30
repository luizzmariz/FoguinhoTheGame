using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : Interactable
{
    public Dialogue dialogue;
    public Sprite dialogueSprite;

    protected override void Interact()
    {
        Debug.Log(dialogue.name);

        GameObject.Find("DialogueManager").GetComponent<DialogueManager>().StartDialogue(dialogue, dialogueSprite);
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue, dialogueSprite);

        Debug.Log("555");
    }

    public override string GetPromptMessage()
    {
        return "talk with " + promptMessage;
    }
}
