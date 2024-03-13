using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioInteractable : Interactable
{
    protected override void Interact()
    {
        Debug.Log("You interacted with " + promptMessage);
    }
}