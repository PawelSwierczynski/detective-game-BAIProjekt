using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueTextBox;
    public Image dialogueBoxShadow;

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
        if (Input.GetKeyDown(KeyCode.F) && dialogueBoxState == DialogueBoxState.Hidden)
        {
            ShowDialogueBox();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && dialogueBoxState == DialogueBoxState.Shown)
        {
            HideDialogueBox();
        }
    }

    public void ShowDialogueBox()
    {
        dialogueTextBox.gameObject.SetActive(true);
        dialogueBoxShadow.gameObject.SetActive(true);

        dialogueBoxState = DialogueBoxState.Shown;
    }

    public void HideDialogueBox()
    {
        dialogueTextBox.gameObject.SetActive(false);
        dialogueBoxShadow.gameObject.SetActive(false);

        dialogueBoxState = DialogueBoxState.Hidden;
    }
}