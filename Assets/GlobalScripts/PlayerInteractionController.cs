using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private Interactable currentInteractable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
            currentInteractable = interactable;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable == currentInteractable)
            currentInteractable = null;
    }

    void Update()
    {
        // E pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // If dialogue is active → continue dialogue
            if (DialogueManager.Instance != null && DialogueManager.Instance.DialogueActive())
            {
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
}
