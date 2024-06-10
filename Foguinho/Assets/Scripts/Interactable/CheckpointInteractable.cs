using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointInteractable : Interactable
{
    public DayCycleManager dayCycleManager;
    [SerializeField] private int hoursToPass;

    void Start()
    {
        dayCycleManager = GameObject.Find("DayCycleManager").GetComponent<DayCycleManager>();
    }

    protected override void Interact()
    {
       // dayCycleManager.ControlDayCycle();
       dayCycleManager.AdvanceTime(hoursToPass);
    }

    public override string GetPromptMessage()
    {
        return "interact with " + promptMessage;
    }
}