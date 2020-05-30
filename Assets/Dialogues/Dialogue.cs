using System.Collections.Generic;

namespace Assets.Dialogues
{
    public class Dialogue
    {
        private IList<DialoguePart> dialogueParts;

        public Dialogue(IList<DialoguePart> dialogueParts)
        {
            this.dialogueParts = dialogueParts;
        }
    }
}