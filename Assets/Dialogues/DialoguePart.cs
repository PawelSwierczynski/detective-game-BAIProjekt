using System.Collections.Generic;

namespace Assets.Dialogues
{
    public class DialoguePart
    {
        public string Speech { get; }
        public IList<Response> Responses { get; }

        public DialoguePart(string speech, IList<Response> responses)
        {
            Speech = speech;
            Responses = responses;
        }
    }
}