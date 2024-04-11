using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interact with objects in space")]
    [SerializeField] float checkSphereRadius = 3f;
    private Collider closestCollider;
    public TMP_Text interactionText;
    public DialogueManager dialogueManager;
    //public KeyCode interactionKey;

    void Start()
    {
        interactionText.text = "";
    }

    void Update()
    {
        closestCollider = null;
        CheckClosestCollider();
    }

    public void OnInteractWithAmbient()
    {
        if(closestCollider != null && !dialogueManager.animator.GetBool("DialogueBoxIsOpen"))
        {
            closestCollider.GetComponent<Interactable>().BaseInteract();
        }
    }

    public void OnEnter()
    {
        if(dialogueManager.animator.GetBool("DialogueBoxIsOpen"))
        {
            dialogueManager.DisplayNextSentence();
        }
    }

    void CheckClosestCollider()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkSphereRadius);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.GetComponent<Interactable>() != null)
            {
                if(closestCollider ==  null)
                {
                    closestCollider = hitCollider;
                }
                else
                {
                    if(Vector3.Distance(transform.position, closestCollider.transform.position) < Vector3.Distance(transform.position, hitCollider.transform.position))
                    {
                        closestCollider = hitCollider;
                    }
                }
            }
        }
        if(closestCollider != null && !dialogueManager.animator.GetBool("DialogueBoxIsOpen"))
        {        
            PlayerInput playerInput = GetComponent<PlayerInput>();
            interactionText.text = "Press " + playerInput.actions["InteractWithAmbient"].GetBindingDisplayString(group: playerInput.currentControlScheme)  + " to interact with " + closestCollider.GetComponent<Interactable>().promptMessage;
        }
        else 
        {
            interactionText.text = "";
        }
    }
}