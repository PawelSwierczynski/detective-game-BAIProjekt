using Assets.Dialogues;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueTextBox;
    public Image dialogueBoxShadow;
    public SC_CharacterController characterController;
    public SC_InventorySystem inventorySystem;

    private Dialogue currentDialogue;
    private DialogueBoxState dialogueBoxState;
    private IDictionary<int, Dialogue> dialogues;

    private enum DialogueBoxState
    {
        Shown,
        Hidden
    }

    void Start()
    {
        HideDialogueBox();

        currentDialogue = null;
        dialogueBoxState = DialogueBoxState.Hidden;
        dialogues = new Dictionary<int, Dialogue>
        {
            [1] = new Dialogue(new Dictionary<int, DialoguePart>
            {
                [0] = new DialoguePart("Zastanawiałeś się kiedyś czy już działają dialogi?", new List<Response>()
                {
                    new Response("Nie.", 1),
                    new Response("Tak, działają?", 2)
                }),
                [1] = new DialoguePart("W każdym razie działają.", new List<Response>()),
                [2] = new DialoguePart("Działają, zdecydowanie.", new List<Response>()
                {
                    new Response("O czym mówiłeś przed chwilą?", 0)
                })
            })
        };
    }

    void Update()
    {
        if (dialogueBoxState == DialogueBoxState.Shown)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HideDialogueBox();
                currentDialogue.ResetProgress();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && currentDialogue.IsResponseAvailable(0))
            {
                currentDialogue.AdvanceToNextSpeech(0);

                UpdateDialogueBox();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && currentDialogue.IsResponseAvailable(1))
            {
                currentDialogue.AdvanceToNextSpeech(1);

                UpdateDialogueBox();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && currentDialogue.IsResponseAvailable(2))
            {
                currentDialogue.AdvanceToNextSpeech(2);

                UpdateDialogueBox();
            }
        }
    }

    private void UpdateDialogueBox()
    {
        dialogueTextBox.text = currentDialogue.RetrieveCurrentSpeech() + "\n\n";

        int responseNumber = 1;

        foreach (var responseText in currentDialogue.RetrieveResponses())
        {
            dialogueTextBox.text += responseNumber.ToString() + ". " + responseText + "\n";
            responseNumber++;
        }
    }

    public void ShowDialogueBox(int dialogueIdentifier)
    {
        currentDialogue = dialogues[dialogueIdentifier];

        UpdateDialogueBox();

        dialogueTextBox.gameObject.SetActive(true);
        dialogueBoxShadow.gameObject.SetActive(true);
        characterController.enabled = false;
        inventorySystem.enabled = false;

        dialogueBoxState = DialogueBoxState.Shown;
    }

    private void HideDialogueBox()
    {
        dialogueTextBox.gameObject.SetActive(false);
        dialogueBoxShadow.gameObject.SetActive(false);
        characterController.enabled = true;
        inventorySystem.enabled = true;

        dialogueBoxState = DialogueBoxState.Hidden;
    }
}