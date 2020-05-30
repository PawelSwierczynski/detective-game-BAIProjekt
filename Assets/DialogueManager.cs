using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueTextBox;
    public Image dialogueBoxShadow;
    public SC_CharacterController characterController;
    public SC_InventorySystem inventorySystem;

    private DialogueBoxState dialogueBoxState;

    private enum DialogueBoxState
    {
        Shown,
        Hidden
    }

    public void Start()
    {
        HideDialogueBox();

        dialogueBoxState = DialogueBoxState.Hidden;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && dialogueBoxState == DialogueBoxState.Shown)
        {
            HideDialogueBox();
        }
    }

    public void ShowDialogueBox()
    {
        dialogueTextBox.gameObject.SetActive(true);
        dialogueBoxShadow.gameObject.SetActive(true);
        characterController.enabled = false;
        inventorySystem.enabled = false;

        dialogueBoxState = DialogueBoxState.Shown;
    }

    public void HideDialogueBox()
    {
        dialogueTextBox.gameObject.SetActive(false);
        dialogueBoxShadow.gameObject.SetActive(false);
        characterController.enabled = true;
        inventorySystem.enabled = true;

        dialogueBoxState = DialogueBoxState.Hidden;
    }
}