using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject player;
    public DialogueManager dialogueManager;
    public int dialogueIdentifier;

    public void OnTriggerStay(Collider collider)
    {
        if (Input.GetKeyDown(KeyCode.F) && collider.name == player.name)
        {
            dialogueManager.ShowDialogueBox(dialogueIdentifier);
        }
    }
}