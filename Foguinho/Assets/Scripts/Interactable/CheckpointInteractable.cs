using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointInteractable : Interactable
{
    public DayCycleManager dayCycleManager;

    void Start()
    {
        dayCycleManager = GameObject.Find("DayCycleManager").GetComponent<DayCycleManager>();
    }

    protected override void Interact()
    {
        dayCycleManager.ControlDayCycle();
    }

    public override string GetPromptMessage()
    {
        return "interact with " + promptMessage;
    }
}