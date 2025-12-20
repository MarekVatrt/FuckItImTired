using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;



public class PlayerInteractionController : MonoBehaviour
{
    private DialogueGiver currentInteractable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        DialogueGiver interactable = other.GetComponent<DialogueGiver>();
        if (interactable != null)
            currentInteractable = interactable;

        
        // after player steps in monologue gameobject, display first message
        ShowDialogue();

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DialogueGiver interactable = other.GetComponent<DialogueGiver>();
        if (interactable == currentInteractable)
            currentInteractable = null;
        // disable dialogue if player exits the gameobject
        if (DialogueManager.Instance != null)
            DialogueManager.Instance.EndDialogue();
    }

    void Update()
    {
        // E pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentInteractable != null)
                currentInteractable.isTriggeredByPlayer = true;
            ShowDialogue();
        }
        else if (DialogueManager.Instance != null && !DialogueManager.Instance.DialogueActive())
        {
            ShowDialogue();
        }
    }

    void ShowDialogue()
    {
        // If dialogue is active continue dialogue
        if (DialogueManager.Instance != null && DialogueManager.Instance.DialogueActive())
        {
            Debug.Log("Dialog je aktivny, pokracuj v dialogu");
            // dialog je aktivovany, lockni hraca na mieste
            Player_controller.inputLocked = true;
            DialogueManager.Instance.ContinueDialogue();
            return;
        }

        // Interact with NPC
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }

    }
}
