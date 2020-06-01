using System.Collections.Generic;

namespace Assets.Dialogues
{
    public class Dialogue
    {
        private int currentDialoguePartIdentifier;
        private readonly IDictionary<int, DialoguePart> dialogueParts;
        public string SpeecherName { get; }

        public Dialogue(string speecherName, IDictionary<int, DialoguePart> dialogueParts)
        {
            currentDialoguePartIdentifier = 0;
            this.dialogueParts = dialogueParts;
            SpeecherName = speecherName;
        }

        public void ResetProgress()
        {
            currentDialoguePartIdentifier = 0;
        }

        public string RetrieveCurrentSpeech()
        {
            return dialogueParts[currentDialoguePartIdentifier].Speech;
        }

        public IList<string> RetrieveResponses()
        {
            return dialogueParts[currentDialoguePartIdentifier].RetrieveResponses();
        }

        public bool IsResponseAvailable(int responseIdentifier)
        {
            return dialogueParts[currentDialoguePartIdentifier].IsResponseAvailable(responseIdentifier);
        }

        public void AdvanceToNextSpeech(int responseIdentifier)
        {
            currentDialoguePartIdentifier = dialogueParts[currentDialoguePartIdentifier].RetrieveNextDialoguePartIdentifier(responseIdentifier);
        }
    }
}